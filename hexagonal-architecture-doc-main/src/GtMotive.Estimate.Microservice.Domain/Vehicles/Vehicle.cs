#nullable enable
using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle aggregate root.
    /// </summary>
    public class Vehicle
    {
        private const int MaximumVehicleAgeInYears = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="id">Vehicle identifier.</param>
        /// <param name="licensePlate">Vehicle license plate.</param>
        /// <param name="brand">Vehicle brand.</param>
        /// <param name="model">Vehicle model.</param>
        /// <param name="manufactureDate">Vehicle manufacture date.</param>
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

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public VehicleId Id { get; }

        /// <summary>
        /// Gets the vehicle license plate.
        /// </summary>
        public LicensePlate LicensePlate { get; }

        /// <summary>
        /// Gets the vehicle brand.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the manufacture date.
        /// </summary>
        public DateOnly ManufactureDate { get; }

        /// <summary>
        /// Gets the current vehicle status.
        /// </summary>
        public VehicleStatus Status { get; private set; }

        /// <summary>
        /// Gets the current rental customer identifier, when any.
        /// </summary>
        public string? CurrentRentalCustomerId { get; private set; }

        /// <summary>
        /// Rents the vehicle to a customer.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        public void Rent(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new DomainException("Customer id cannot be empty.");
            }

            if (this.Status != VehicleStatus.Available)
            {
                throw new VehicleNotAvailableException(this.Id);
            }

            this.Status = VehicleStatus.Rented;
            this.CurrentRentalCustomerId = customerId.Trim();
        }

        /// <summary>
        /// Returns the vehicle to available status.
        /// </summary>
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
