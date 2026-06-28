using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ReturnVehicle
{
    public class ReturnVehicleOutput : IUseCaseOutput
    {
        public ReturnVehicleOutput(
            VehicleId vehicleId,
            string licensePlate,
            VehicleStatus status)
        {
            this.VehicleId = vehicleId;
            this.LicensePlate = licensePlate;
            this.Status = status;
        }

        public VehicleId VehicleId { get; }

        public string LicensePlate { get; }

        public VehicleStatus Status { get; }
    }
}
