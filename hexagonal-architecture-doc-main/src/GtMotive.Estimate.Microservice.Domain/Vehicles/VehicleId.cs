using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    public readonly record struct VehicleId(Guid Value)
    {
        public static VehicleId New() => new(Guid.NewGuid());

        public static VehicleId From(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Vehicle id cannot be empty.");
            }

            return new VehicleId(value);
        }

        public override string ToString() => this.Value.ToString();
    }
}
