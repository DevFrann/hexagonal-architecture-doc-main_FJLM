using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle
{
    public class CreateVehicleInput : IUseCaseInput
    {
        public CreateVehicleInput(
            string licensePlate,
            string brand,
            string model,
            DateOnly manufactureDate)
        {
            this.LicensePlate = licensePlate;
            this.Brand = brand;
            this.Model = model;
            this.ManufactureDate = manufactureDate;
        }

        public string LicensePlate { get; }

        public string Brand { get; }

        public string Model { get; }

        public DateOnly ManufactureDate { get; }
    }
}
