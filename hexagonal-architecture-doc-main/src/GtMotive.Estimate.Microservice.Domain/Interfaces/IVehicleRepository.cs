using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Vehicle persistence port.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">Vehicle to persist.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a vehicle by identifier.
        /// </summary>
        /// <param name="id">Vehicle identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The vehicle when found; otherwise <see langword="null"/>.</returns>
        Task<Vehicle?> GetByIdAsync(VehicleId id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a vehicle by license plate.
        /// </summary>
        /// <param name="licensePlate">Vehicle license plate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The vehicle when found; otherwise <see langword="null"/>.</returns>
        Task<Vehicle?> GetByLicensePlateAsync(LicensePlate licensePlate, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all available vehicles.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Available vehicles.</returns>
        Task<IReadOnlyCollection<Vehicle>> GetAvailableAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Checks whether the customer has any active rental.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><see langword="true"/> when the customer has an active rental; otherwise <see langword="false"/>.</returns>
        Task<bool> HasActiveRentalByCustomerAsync(string customerId, CancellationToken cancellationToken);

        /// <summary>
        /// Persists pending repository changes.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
