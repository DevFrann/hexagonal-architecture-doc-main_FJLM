namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Exception raised when a license plate is invalid.
    /// </summary>
    public class InvalidLicensePlateException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLicensePlateException"/> class.
        /// </summary>
        public InvalidLicensePlateException()
            : base("License plate cannot be empty.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLicensePlateException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidLicensePlateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLicensePlateException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InvalidLicensePlateException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
