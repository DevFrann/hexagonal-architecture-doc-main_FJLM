using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    internal sealed class TestWebApplicationFactory : WebApplicationFactory<WebApplicationFactoryEntryPoint>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("IntegrationTest");
            builder.UseStartup<Startup>();
        }
    }
}
