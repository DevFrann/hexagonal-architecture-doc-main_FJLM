namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    public class VehicleNotRentedException : DomainException
    {
        public VehicleNotRentedException(VehicleId vehicleId)
            : base($"Vehicle '{vehicleId}' is not currently rented.")
        {
        }
    }
}
