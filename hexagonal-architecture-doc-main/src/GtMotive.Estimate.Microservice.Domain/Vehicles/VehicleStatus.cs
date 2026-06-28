namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle availability status.
    /// </summary>
    public enum VehicleStatus
    {
        /// <summary>
        /// Vehicle is available for renting.
        /// </summary>
        Available = 0,

        /// <summary>
        /// Vehicle is currently rented.
        /// </summary>
        Rented = 1,
    }
}
