using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    public sealed class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "src", "GtMotive.Estimate.Microservice.Host")));
            builder.UseEnvironment("Development");
        }
    }
}
