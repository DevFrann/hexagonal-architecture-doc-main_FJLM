#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    public class InMemoryVehicleRepository : IVehicleRepository
    {
        private readonly object syncRoot = new();
        private readonly List<Vehicle> vehicles = [];

        public Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            lock (this.syncRoot)
            {
                this.vehicles.Add(vehicle);
            }

            return Task.CompletedTask;
        }

        public Task<Vehicle?> GetByIdAsync(VehicleId id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            lock (this.syncRoot)
            {
                return Task.FromResult(this.vehicles.Find(vehicle => vehicle.Id == id));
            }
        }

        public Task<Vehicle?> GetByLicensePlateAsync(LicensePlate licensePlate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            lock (this.syncRoot)
            {
                return Task.FromResult(this.vehicles.Find(vehicle => vehicle.LicensePlate == licensePlate));
            }
        }

        public Task<IReadOnlyCollection<Vehicle>> GetAvailableAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            lock (this.syncRoot)
            {
                IReadOnlyCollection<Vehicle> availableVehicles = this.vehicles
                    .Where(vehicle => vehicle.Status == VehicleStatus.Available)
                    .ToArray();

                return Task.FromResult(availableVehicles);
            }
        }

        public Task<bool> HasActiveRentalByCustomerAsync(string customerId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            lock (this.syncRoot)
            {
                return Task.FromResult(this.vehicles.Exists(vehicle =>
                    vehicle.Status == VehicleStatus.Rented &&
                    string.Equals(vehicle.CurrentRentalCustomerId, customerId, StringComparison.Ordinal)));
            }
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }
    }
}
