using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ReturnVehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public class ReturnVehiclePresenter : IReturnVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        public void StandardHandle(ReturnVehicleOutput response)
        {
            this.ActionResult = new OkObjectResult(response);
        }

        public void NotFoundHandle(string message)
        {
            this.ActionResult = new NotFoundObjectResult(new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = message,
            });
        }
    }
}
