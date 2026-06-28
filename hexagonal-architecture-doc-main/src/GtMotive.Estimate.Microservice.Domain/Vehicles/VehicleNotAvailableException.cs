namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    public class VehicleNotAvailableException : DomainException
    {
        public VehicleNotAvailableException(VehicleId vehicleId)
            : base($"Vehicle '{vehicleId}' is not available for renting.")
        {
        }
    }
}
