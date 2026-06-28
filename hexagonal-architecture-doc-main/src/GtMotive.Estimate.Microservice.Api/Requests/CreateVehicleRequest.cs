#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Requests
{
    public class CreateVehicleRequest
    {
        [Required]
        public string? LicensePlate { get; init; }

        [Required]
        public string? Brand { get; init; }

        [Required]
        public string? Model { get; init; }

        [Required]
        public DateOnly? ManufactureDate { get; init; }
    }
}
