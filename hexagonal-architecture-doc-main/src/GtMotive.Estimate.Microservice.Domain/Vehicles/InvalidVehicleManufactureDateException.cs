using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    public class InvalidVehicleManufactureDateException : DomainException
    {
        public InvalidVehicleManufactureDateException(DateOnly manufactureDate, int maximumVehicleAgeInYears)
            : base($"Manufacture date '{manufactureDate:yyyy-MM-dd}' is invalid. Vehicles must be no older than {maximumVehicleAgeInYears} years.")
        {
        }
    }
}
