namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ReturnVehicle
{
    public class ReturnVehicleInput : IUseCaseInput
    {
        public ReturnVehicleInput(string vehicleId)
        {
            this.VehicleId = vehicleId;
        }

        public string VehicleId { get; }
    }
}
