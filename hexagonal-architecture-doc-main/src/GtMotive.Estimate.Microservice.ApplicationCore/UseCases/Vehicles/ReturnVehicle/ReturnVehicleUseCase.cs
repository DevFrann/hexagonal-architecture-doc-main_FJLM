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
        private readonly IOutputPortStandard<ReturnVehicleOutput> outputPort;
        private readonly IOutputPortNotFound notFoundOutputPort;

        public ReturnVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IOutputPortStandard<ReturnVehicleOutput> outputPort,
            IOutputPortNotFound notFoundOutputPort)
        {
            this.vehicleRepository = vehicleRepository;
            this.outputPort = outputPort;
            this.notFoundOutputPort = notFoundOutputPort;
        }

        public async Task Execute(ReturnVehicleInput input)
        {
            var cancellationToken = CancellationToken.None;
            var vehicleId = ParseVehicleId(input.VehicleId);

            var vehicle = await this.vehicleRepository
                .GetByIdAsync(vehicleId, cancellationToken)
                .ConfigureAwait(false);

            if (vehicle is null)
            {
                this.notFoundOutputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
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
