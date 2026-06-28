using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle identifier value object.
    /// </summary>
    public readonly record struct VehicleId(Guid Value)
    {
        /// <summary>
        /// Creates a new vehicle identifier.
        /// </summary>
        /// <returns>A new vehicle identifier.</returns>
        public static VehicleId New() => new(Guid.NewGuid());

        /// <summary>
        /// Creates a vehicle identifier from an existing guid value.
        /// </summary>
        /// <param name="value">Guid value.</param>
        /// <returns>A vehicle identifier.</returns>
        public static VehicleId From(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Vehicle id cannot be empty.");
            }

            return new VehicleId(value);
        }

        /// <summary>
        /// Returns the identifier as text.
        /// </summary>
        /// <returns>Identifier text.</returns>
        public override string ToString() => this.Value.ToString();
    }
}
