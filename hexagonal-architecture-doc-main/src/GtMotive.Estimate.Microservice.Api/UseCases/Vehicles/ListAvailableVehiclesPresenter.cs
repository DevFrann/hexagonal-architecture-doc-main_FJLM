using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public class ListAvailableVehiclesPresenter : IOutputPortStandard<ListAvailableVehiclesOutput>, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        public void StandardHandle(ListAvailableVehiclesOutput response)
        {
            this.ActionResult = new OkObjectResult(response.Vehicles);
        }
    }
}
