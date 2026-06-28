using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle
{
    public class CreateVehicleUseCase : ICreateVehicleUseCase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IOutputPortStandard<CreateVehicleOutput> outputPort;

        public CreateVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IOutputPortStandard<CreateVehicleOutput> outputPort)
        {
            this.vehicleRepository = vehicleRepository;
            this.outputPort = outputPort;
        }

        public async Task Execute(CreateVehicleInput input)
        {
            var cancellationToken = CancellationToken.None;
            var licensePlate = new LicensePlate(input.LicensePlate);

            var existingVehicle = await this.vehicleRepository
                .GetByLicensePlateAsync(licensePlate, cancellationToken)
                .ConfigureAwait(false);

            if (existingVehicle is not null)
            {
                throw new DomainException($"Vehicle with license plate '{licensePlate}' already exists.");
            }

            var vehicle = new Vehicle(
                VehicleId.New(),
                licensePlate,
                input.Brand,
                input.Model,
                input.ManufactureDate);

            await this.vehicleRepository.AddAsync(vehicle, cancellationToken).ConfigureAwait(false);
            await this.vehicleRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            this.outputPort.StandardHandle(new CreateVehicleOutput(
                vehicle.Id,
                vehicle.LicensePlate.Value,
                vehicle.Brand,
                vehicle.Model,
                vehicle.ManufactureDate,
                vehicle.Status));
        }
    }
}
