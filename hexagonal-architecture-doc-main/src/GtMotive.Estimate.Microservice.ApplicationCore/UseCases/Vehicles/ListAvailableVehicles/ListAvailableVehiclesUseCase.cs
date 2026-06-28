using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles
{
    public class ListAvailableVehiclesUseCase : IListAvailableVehiclesUseCase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IOutputPortStandard<ListAvailableVehiclesOutput> outputPort;

        public ListAvailableVehiclesUseCase(
            IVehicleRepository vehicleRepository,
            IOutputPortStandard<ListAvailableVehiclesOutput> outputPort)
        {
            this.vehicleRepository = vehicleRepository;
            this.outputPort = outputPort;
        }

        public async Task Execute(ListAvailableVehiclesInput input)
        {
            var cancellationToken = CancellationToken.None;
            var vehicles = await this.vehicleRepository.GetAvailableAsync(cancellationToken).ConfigureAwait(false);

            var availableVehicles = vehicles
                .Where(vehicle => vehicle.Status == VehicleStatus.Available)
                .Select(vehicle => new AvailableVehicleDto(
                    vehicle.Id,
                    vehicle.LicensePlate.Value,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.ManufactureDate))
                .ToList();

            this.outputPort.StandardHandle(new ListAvailableVehiclesOutput(availableVehicles));
        }
    }
}
