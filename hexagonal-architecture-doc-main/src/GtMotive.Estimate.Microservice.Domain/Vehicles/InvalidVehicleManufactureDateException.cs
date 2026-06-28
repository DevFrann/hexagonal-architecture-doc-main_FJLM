using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Exception raised when a vehicle manufacture date is invalid.
    /// </summary>
    public class InvalidVehicleManufactureDateException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVehicleManufactureDateException"/> class.
        /// </summary>
        public InvalidVehicleManufactureDateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVehicleManufactureDateException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidVehicleManufactureDateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVehicleManufactureDateException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InvalidVehicleManufactureDateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVehicleManufactureDateException"/> class.
        /// </summary>
        /// <param name="manufactureDate">Manufacture date.</param>
        /// <param name="maximumVehicleAgeInYears">Maximum age in years.</param>
        public InvalidVehicleManufactureDateException(DateOnly manufactureDate, int maximumVehicleAgeInYears)
            : base($"Manufacture date '{manufactureDate:yyyy-MM-dd}' is invalid. Vehicles must be no older than {maximumVehicleAgeInYears} years.")
        {
        }
    }
}
