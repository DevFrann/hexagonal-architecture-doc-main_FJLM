namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Exception raised when a vehicle is not available for renting.
    /// </summary>
    public class VehicleNotAvailableException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        public VehicleNotAvailableException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public VehicleNotAvailableException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public VehicleNotAvailableException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        public VehicleNotAvailableException(VehicleId vehicleId)
            : base($"Vehicle '{vehicleId}' is not available for renting.")
        {
        }
    }
}
