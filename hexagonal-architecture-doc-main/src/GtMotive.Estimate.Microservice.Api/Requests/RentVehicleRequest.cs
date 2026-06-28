#nullable enable
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Requests
{
    public class RentVehicleRequest
    {
        [Required]
        public string? CustomerId { get; init; }
    }
}
