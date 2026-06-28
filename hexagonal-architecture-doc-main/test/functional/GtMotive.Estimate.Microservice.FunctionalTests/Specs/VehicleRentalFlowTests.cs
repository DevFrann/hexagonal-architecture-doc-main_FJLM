using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ReturnVehicle;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    public class VehicleRentalFlowTests(CompositionRootTestFixture fixture) : FunctionalTestBase(fixture)
    {
        [Fact]
        public async Task ShouldCreateRentReturnVehicleAcrossRealUseCases()
        {
            string createdVehicleId = string.Empty;
            var customerId = Guid.NewGuid().ToString();

            await this.Fixture.UsingScope(async serviceProvider =>
            {
                var createVehicleUseCase = serviceProvider.GetRequiredService<ICreateVehicleUseCase>();
                var createVehiclePresenter = serviceProvider.GetRequiredService<CreateVehiclePresenter>();

                await createVehicleUseCase.Execute(new CreateVehicleInput(
                    "5678DEF",
                    "Seat",
                    "Leon",
                    DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1))));

                var createResponse = createVehiclePresenter.ActionResult.Should().BeOfType<Microsoft.AspNetCore.Mvc.CreatedResult>()
                    .Subject.Value.Should().BeOfType<CreateVehicleOutput>().Subject;

                createdVehicleId = createResponse.VehicleId.Value.ToString();
            });

            await this.Fixture.UsingScope(async serviceProvider =>
            {
                var listAvailableVehiclesUseCase = serviceProvider.GetRequiredService<IListAvailableVehiclesUseCase>();
                var listAvailableVehiclesPresenter = serviceProvider.GetRequiredService<ListAvailableVehiclesPresenter>();

                await listAvailableVehiclesUseCase.Execute(new ListAvailableVehiclesInput());

                var availableVehicles = listAvailableVehiclesPresenter.ActionResult
                    .Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>()
                    .Subject.Value.Should().BeAssignableTo<IEnumerable<AvailableVehicleDto>>()
                    .Subject;

                availableVehicles.Should().Contain(vehicle => vehicle.Id.Value.ToString() == createdVehicleId);
            });

            await this.Fixture.UsingScope(async serviceProvider =>
            {
                var rentVehicleUseCase = serviceProvider.GetRequiredService<IRentVehicleUseCase>();
                var rentVehiclePresenter = serviceProvider.GetRequiredService<RentVehiclePresenter>();

                await rentVehicleUseCase.Execute(new RentVehicleInput(createdVehicleId, customerId));

                var rentResponse = rentVehiclePresenter.ActionResult
                    .Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>()
                    .Subject.Value.Should().BeOfType<RentVehicleOutput>()
                    .Subject;

                rentResponse.VehicleId.Value.ToString().Should().Be(createdVehicleId);
                rentResponse.CustomerId.Should().Be(customerId);
            });

            await this.Fixture.UsingScope(async serviceProvider =>
            {
                var listAvailableVehiclesUseCase = serviceProvider.GetRequiredService<IListAvailableVehiclesUseCase>();
                var listAvailableVehiclesPresenter = serviceProvider.GetRequiredService<ListAvailableVehiclesPresenter>();

                await listAvailableVehiclesUseCase.Execute(new ListAvailableVehiclesInput());

                var availableVehicles = listAvailableVehiclesPresenter.ActionResult
                    .Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>()
                    .Subject.Value.Should().BeAssignableTo<IEnumerable<AvailableVehicleDto>>()
                    .Subject;

                availableVehicles.Should().NotContain(vehicle => vehicle.Id.Value.ToString() == createdVehicleId);
            });

            await this.Fixture.UsingScope(async serviceProvider =>
            {
                var returnVehicleUseCase = serviceProvider.GetRequiredService<IReturnVehicleUseCase>();
                var returnVehiclePresenter = serviceProvider.GetRequiredService<ReturnVehiclePresenter>();

                await returnVehicleUseCase.Execute(new ReturnVehicleInput(createdVehicleId));

                var returnResponse = returnVehiclePresenter.ActionResult
                    .Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>()
                    .Subject.Value.Should().BeOfType<ReturnVehicleOutput>()
                    .Subject;

                returnResponse.VehicleId.Value.ToString().Should().Be(createdVehicleId);
            });

            await this.Fixture.UsingScope(async serviceProvider =>
            {
                var listAvailableVehiclesUseCase = serviceProvider.GetRequiredService<IListAvailableVehiclesUseCase>();
                var listAvailableVehiclesPresenter = serviceProvider.GetRequiredService<ListAvailableVehiclesPresenter>();

                await listAvailableVehiclesUseCase.Execute(new ListAvailableVehiclesInput());

                var availableVehicles = listAvailableVehiclesPresenter.ActionResult
                    .Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>()
                    .Subject.Value.Should().BeAssignableTo<IEnumerable<AvailableVehicleDto>>()
                    .Subject
                    .ToList();

                availableVehicles.Should().Contain(vehicle => vehicle.Id.Value.ToString() == createdVehicleId);
            });
        }
    }
}
