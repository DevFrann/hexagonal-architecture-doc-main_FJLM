#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.RentVehicle;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.UseCases.Vehicles.RentVehicle
{
    public class RentVehicleUseCaseTests
    {
        [Fact]
        public async Task Execute_ShouldRentVehicle_WhenVehicleIsAvailable()
        {
            var vehicle = CreateVehicle();
            var repository = new FakeVehicleRepository([vehicle]);
            var outputPort = new FakeRentVehicleOutputPort();
            var useCase = new RentVehicleUseCase(repository, outputPort);
            var customerId = "customer-001";

            await useCase.Execute(new RentVehicleInput(vehicle.Id.Value.ToString(), customerId));

            vehicle.Status.Should().Be(VehicleStatus.Rented);
            vehicle.CurrentRentalCustomerId.Should().Be(customerId);
            repository.SaveChangesCalled.Should().BeTrue();
            outputPort.StandardResponse.Should().NotBeNull();
            outputPort.StandardResponse!.VehicleId.Should().Be(vehicle.Id);
            outputPort.StandardResponse.LicensePlate.Should().Be(vehicle.LicensePlate.Value);
            outputPort.StandardResponse.CustomerId.Should().Be(customerId);
            outputPort.StandardResponse.Status.Should().Be(VehicleStatus.Rented);
            outputPort.NotFoundMessage.Should().BeNull();
        }

        [Fact]
        public async Task Execute_ShouldThrowDomainException_WhenCustomerAlreadyHasAnActiveRental()
        {
            var vehicle = CreateVehicle();
            var customerId = "customer-002";
            var repository = new FakeVehicleRepository([vehicle])
            {
                HasActiveRentalByCustomerResult = true,
            };
            var outputPort = new FakeRentVehicleOutputPort();
            var useCase = new RentVehicleUseCase(repository, outputPort);

            var act = async () => await useCase.Execute(new RentVehicleInput(vehicle.Id.Value.ToString(), customerId));

            await act.Should().ThrowAsync<DomainException>()
                .WithMessage($"Customer '{customerId}' already has an active rental.");
            vehicle.Status.Should().Be(VehicleStatus.Available);
            repository.SaveChangesCalled.Should().BeFalse();
            outputPort.StandardResponse.Should().BeNull();
            outputPort.NotFoundMessage.Should().BeNull();
        }

        [Fact]
        public async Task Execute_ShouldReturnNotFound_WhenVehicleDoesNotExist()
        {
            var repository = new FakeVehicleRepository([]);
            var outputPort = new FakeRentVehicleOutputPort();
            var useCase = new RentVehicleUseCase(repository, outputPort);
            var vehicleId = Guid.NewGuid().ToString();
            var customerId = "customer-003";

            await useCase.Execute(new RentVehicleInput(vehicleId, customerId));

            outputPort.NotFoundMessage.Should().Be($"Vehicle '{vehicleId}' was not found.");
            outputPort.StandardResponse.Should().BeNull();
            repository.SaveChangesCalled.Should().BeFalse();
        }

        private static Vehicle CreateVehicle()
        {
            return new Vehicle(
                VehicleId.New(),
                new LicensePlate("1234ABC"),
                "Toyota",
                "Corolla",
                DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1)));
        }

        private sealed class FakeRentVehicleOutputPort : IRentVehicleOutputPort
        {
            public string? NotFoundMessage { get; private set; }

            public RentVehicleOutput? StandardResponse { get; private set; }

            public void NotFoundHandle(string message)
            {
                this.NotFoundMessage = message;
            }

            public void StandardHandle(RentVehicleOutput response)
            {
                this.StandardResponse = response;
            }
        }

        private sealed class FakeVehicleRepository : IVehicleRepository
        {
            private readonly List<Vehicle> vehicles;

            public FakeVehicleRepository(IEnumerable<Vehicle> vehicles)
            {
                this.vehicles = vehicles.ToList();
            }

            public bool HasActiveRentalByCustomerResult { get; init; }

            public bool SaveChangesCalled { get; private set; }

            public Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
            {
                this.vehicles.Add(vehicle);
                return Task.CompletedTask;
            }

            public Task<Vehicle?> GetByIdAsync(VehicleId id, CancellationToken cancellationToken)
            {
                return Task.FromResult(this.vehicles.Find(vehicle => vehicle.Id == id));
            }

            public Task<Vehicle?> GetByLicensePlateAsync(LicensePlate licensePlate, CancellationToken cancellationToken)
            {
                return Task.FromResult(this.vehicles.Find(vehicle => vehicle.LicensePlate == licensePlate));
            }

            public Task<IReadOnlyCollection<Vehicle>> GetAvailableAsync(CancellationToken cancellationToken)
            {
                IReadOnlyCollection<Vehicle> result = new ReadOnlyCollection<Vehicle>(
                    this.vehicles.Where(vehicle => vehicle.Status == VehicleStatus.Available).ToList());

                return Task.FromResult(result);
            }

            public Task<bool> HasActiveRentalByCustomerAsync(string customerId, CancellationToken cancellationToken)
            {
                return Task.FromResult(this.HasActiveRentalByCustomerResult);
            }

            public Task SaveChangesAsync(CancellationToken cancellationToken)
            {
                this.SaveChangesCalled = true;
                return Task.CompletedTask;
            }
        }
    }
}
