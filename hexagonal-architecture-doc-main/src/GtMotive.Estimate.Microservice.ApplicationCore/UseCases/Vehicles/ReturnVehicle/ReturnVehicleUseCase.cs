using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ReturnVehicle
{
    public class ReturnVehicleUseCase : IReturnVehicleUseCase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IReturnVehicleOutputPort outputPort;

        public ReturnVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IReturnVehicleOutputPort outputPort)
        {
            this.vehicleRepository = vehicleRepository;
            this.outputPort = outputPort;
        }

        public async Task Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var cancellationToken = CancellationToken.None;
            var vehicleId = ParseVehicleId(input.VehicleId);

            var vehicle = await this.vehicleRepository
                .GetByIdAsync(vehicleId, cancellationToken)
                .ConfigureAwait(false);

            if (vehicle is null)
            {
                this.outputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
                return;
            }

            vehicle.Return();

            await this.vehicleRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            this.outputPort.StandardHandle(new ReturnVehicleOutput(
                vehicle.Id,
                vehicle.LicensePlate.Value,
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
    }
}
