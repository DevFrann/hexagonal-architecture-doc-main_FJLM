using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.RentVehicle
{
    public class RentVehicleUseCase : IRentVehicleUseCase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IOutputPortStandard<RentVehicleOutput> outputPort;
        private readonly IOutputPortNotFound notFoundOutputPort;

        public RentVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IOutputPortStandard<RentVehicleOutput> outputPort,
            IOutputPortNotFound notFoundOutputPort)
        {
            this.vehicleRepository = vehicleRepository;
            this.outputPort = outputPort;
            this.notFoundOutputPort = notFoundOutputPort;
        }

        public async Task Execute(RentVehicleInput input)
        {
            var cancellationToken = CancellationToken.None;
            var vehicleId = ParseVehicleId(input.VehicleId);
            var customerId = ParseCustomerId(input.CustomerId);

            var vehicle = await this.vehicleRepository
                .GetByIdAsync(vehicleId, cancellationToken)
                .ConfigureAwait(false);

            if (vehicle is null)
            {
                this.notFoundOutputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
                return;
            }

            var hasActiveRental = await this.vehicleRepository
                .HasActiveRentalByCustomerAsync(input.CustomerId, cancellationToken)
                .ConfigureAwait(false);

            if (hasActiveRental)
            {
                throw new DomainException($"Customer '{input.CustomerId}' already has an active rental.");
            }

            vehicle.Rent(customerId);

            await this.vehicleRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            this.outputPort.StandardHandle(new RentVehicleOutput(
                vehicle.Id,
                vehicle.LicensePlate.Value,
                input.CustomerId,
                vehicle.Status));
        }

        private static VehicleId ParseVehicleId(string vehicleId)
        {
            if (!Guid.TryParse(vehicleId, out var parsedVehicleId))
            {
                throw new DomainException("Vehicle id is invalid.");
            }

            return VehicleId.From(parsedVehicleId);
        }

        private static Guid ParseCustomerId(string customerId)
        {
            if (!Guid.TryParse(customerId, out var parsedCustomerId))
            {
                throw new DomainException("Customer id is invalid.");
            }

            return parsedCustomerId;
        }
    }
}
