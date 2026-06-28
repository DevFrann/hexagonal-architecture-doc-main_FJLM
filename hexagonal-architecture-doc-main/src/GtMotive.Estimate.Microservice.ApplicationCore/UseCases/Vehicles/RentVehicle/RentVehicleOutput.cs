using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.RentVehicle
{
    public class RentVehicleOutput : IUseCaseOutput
    {
        public RentVehicleOutput(
            VehicleId vehicleId,
            string licensePlate,
            string customerId,
            VehicleStatus status)
        {
            this.VehicleId = vehicleId;
            this.LicensePlate = licensePlate;
            this.CustomerId = customerId;
            this.Status = status;
        }

        public VehicleId VehicleId { get; }

        public string LicensePlate { get; }

        public string CustomerId { get; }

        public VehicleStatus Status { get; }
    }
}
