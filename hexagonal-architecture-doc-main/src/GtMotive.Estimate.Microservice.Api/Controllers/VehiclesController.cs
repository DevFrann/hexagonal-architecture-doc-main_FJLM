using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Requests;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ReturnVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly ICreateVehicleUseCase createVehicleUseCase;
        private readonly CreateVehiclePresenter createVehiclePresenter;
        private readonly IListAvailableVehiclesUseCase listAvailableVehiclesUseCase;
        private readonly ListAvailableVehiclesPresenter listAvailableVehiclesPresenter;
        private readonly IRentVehicleUseCase rentVehicleUseCase;
        private readonly RentVehiclePresenter rentVehiclePresenter;
        private readonly IReturnVehicleUseCase returnVehicleUseCase;
        private readonly ReturnVehiclePresenter returnVehiclePresenter;

        public VehiclesController(
            ICreateVehicleUseCase createVehicleUseCase,
            CreateVehiclePresenter createVehiclePresenter,
            IListAvailableVehiclesUseCase listAvailableVehiclesUseCase,
            ListAvailableVehiclesPresenter listAvailableVehiclesPresenter,
            IRentVehicleUseCase rentVehicleUseCase,
            RentVehiclePresenter rentVehiclePresenter,
            IReturnVehicleUseCase returnVehicleUseCase,
            ReturnVehiclePresenter returnVehiclePresenter)
        {
            this.createVehicleUseCase = createVehicleUseCase;
            this.createVehiclePresenter = createVehiclePresenter;
            this.listAvailableVehiclesUseCase = listAvailableVehiclesUseCase;
            this.listAvailableVehiclesPresenter = listAvailableVehiclesPresenter;
            this.rentVehicleUseCase = rentVehicleUseCase;
            this.rentVehiclePresenter = rentVehiclePresenter;
            this.returnVehicleUseCase = returnVehicleUseCase;
            this.returnVehiclePresenter = returnVehiclePresenter;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request)
        {
            System.ArgumentNullException.ThrowIfNull(request);

            await this.createVehicleUseCase.Execute(new CreateVehicleInput(
                request.LicensePlate!,
                request.Brand!,
                request.Model!,
                request.ManufactureDate!.Value)).ConfigureAwait(false);

            return this.createVehiclePresenter.ActionResult;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            await this.listAvailableVehiclesUseCase.Execute(new ListAvailableVehiclesInput()).ConfigureAwait(false);

            return this.listAvailableVehiclesPresenter.ActionResult;
        }

        [HttpPost("{vehicleId}/rent")]
        public async Task<IActionResult> Rent(string vehicleId, [FromBody] RentVehicleRequest request)
        {
            System.ArgumentNullException.ThrowIfNull(request);

            await this.rentVehicleUseCase.Execute(new RentVehicleInput(vehicleId, request.CustomerId!)).ConfigureAwait(false);

            return this.rentVehiclePresenter.ActionResult;
        }

        [HttpPost("{vehicleId}/return")]
        public async Task<IActionResult> Return(string vehicleId)
        {
            await this.returnVehicleUseCase.Execute(new ReturnVehicleInput(vehicleId)).ConfigureAwait(false);

            return this.returnVehiclePresenter.ActionResult;
        }
    }
}
