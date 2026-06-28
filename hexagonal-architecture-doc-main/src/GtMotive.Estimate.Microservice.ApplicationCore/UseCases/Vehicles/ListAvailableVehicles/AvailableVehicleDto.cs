using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles
{
    public class AvailableVehicleDto
    {
        public AvailableVehicleDto(
            VehicleId id,
            string licensePlate,
            string brand,
            string model,
            DateOnly manufactureDate)
        {
            this.Id = id;
            this.LicensePlate = licensePlate;
            this.Brand = brand;
            this.Model = model;
            this.ManufactureDate = manufactureDate;
        }

        public VehicleId Id { get; }

        public string LicensePlate { get; }

        public string Brand { get; }

        public string Model { get; }

        public DateOnly ManufactureDate { get; }
    }
}
