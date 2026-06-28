namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Exception raised when a vehicle is not currently rented.
    /// </summary>
    public class VehicleNotRentedException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotRentedException"/> class.
        /// </summary>
        public VehicleNotRentedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotRentedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public VehicleNotRentedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotRentedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public VehicleNotRentedException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotRentedException"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        public VehicleNotRentedException(VehicleId vehicleId)
            : base($"Vehicle '{vehicleId}' is not currently rented.")
        {
        }
    }
}
