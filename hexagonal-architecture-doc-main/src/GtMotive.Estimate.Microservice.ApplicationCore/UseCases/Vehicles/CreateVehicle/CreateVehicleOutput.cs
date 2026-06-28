using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle
{
    public class CreateVehicleOutput : IUseCaseOutput
    {
        public CreateVehicleOutput(
            VehicleId vehicleId,
            string licensePlate,
            string brand,
            string model,
            DateOnly manufactureDate,
            VehicleStatus status)
        {
            this.VehicleId = vehicleId;
            this.LicensePlate = licensePlate;
            this.Brand = brand;
            this.Model = model;
            this.ManufactureDate = manufactureDate;
            this.Status = status;
        }

        public VehicleId VehicleId { get; }

        public string LicensePlate { get; }

        public string Brand { get; }

        public string Model { get; }

        public DateOnly ManufactureDate { get; }

        public VehicleStatus Status { get; }
    }
}
