using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    public class VehiclesEndpointValidationTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient httpClient;

        public VehiclesEndpointValidationTests(TestWebApplicationFactory factory)
        {
            this.httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task PostVehicles_ShouldReturnBadRequest_WhenLicensePlateIsMissing()
        {
            var requestBody = JsonSerializer.Serialize(new
            {
                brand = "Seat",
                model = "Leon",
                manufactureDate = "2025-01-01",
            });
            using var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            using var response = await this.httpClient.PostAsync("/api/vehicles", content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
