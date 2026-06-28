using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    public class Vehicle
    {
        private const int MaximumVehicleAgeInYears = 5;

        public Vehicle(
            VehicleId id,
            LicensePlate licensePlate,
            string brand,
            string model,
            DateOnly manufactureDate)
        {
            if (id.Value == Guid.Empty)
            {
                throw new DomainException("Vehicle id cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new DomainException("Vehicle brand cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new DomainException("Vehicle model cannot be empty.");
            }

            ValidateManufactureDate(manufactureDate);

            this.Id = id;
            this.LicensePlate = licensePlate;
            this.Brand = brand.Trim();
            this.Model = model.Trim();
            this.ManufactureDate = manufactureDate;
            this.Status = VehicleStatus.Available;
            this.CurrentRentalCustomerId = null;
        }

        public VehicleId Id { get; }

        public LicensePlate LicensePlate { get; }

        public string Brand { get; }

        public string Model { get; }

        public DateOnly ManufactureDate { get; }

        public VehicleStatus Status { get; private set; }

        public Guid? CurrentRentalCustomerId { get; private set; }

        public void Rent(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new DomainException("Customer id cannot be empty.");
            }

            if (this.Status != VehicleStatus.Available)
            {
                throw new VehicleNotAvailableException(this.Id);
            }

            this.Status = VehicleStatus.Rented;
            this.CurrentRentalCustomerId = customerId;
        }

        public void Return()
        {
            if (this.Status != VehicleStatus.Rented)
            {
                throw new VehicleNotRentedException(this.Id);
            }

            this.Status = VehicleStatus.Available;
            this.CurrentRentalCustomerId = null;
        }

        private static void ValidateManufactureDate(DateOnly manufactureDate)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var oldestAllowedDate = today.AddYears(-MaximumVehicleAgeInYears);

            if (manufactureDate > today)
            {
                throw new InvalidVehicleManufactureDateException(manufactureDate, MaximumVehicleAgeInYears);
            }

            if (manufactureDate < oldestAllowedDate)
            {
                throw new InvalidVehicleManufactureDateException(manufactureDate, MaximumVehicleAgeInYears);
            }
        }
    }
}
