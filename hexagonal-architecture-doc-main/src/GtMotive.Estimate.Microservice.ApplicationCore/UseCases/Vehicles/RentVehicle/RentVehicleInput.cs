namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.RentVehicle
{
    public class RentVehicleInput : IUseCaseInput
    {
        public RentVehicleInput(string vehicleId, string customerId)
        {
            this.VehicleId = vehicleId;
            this.CustomerId = customerId;
        }

        public string VehicleId { get; }

        public string CustomerId { get; }
    }
}
