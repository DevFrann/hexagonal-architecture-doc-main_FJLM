using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public class CreateVehiclePresenter : IOutputPortStandard<CreateVehicleOutput>, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        public void StandardHandle(CreateVehicleOutput response)
        {
            System.ArgumentNullException.ThrowIfNull(response);

            this.ActionResult = new CreatedResult($"/api/vehicles/{response.VehicleId}", response);
        }
    }
}
