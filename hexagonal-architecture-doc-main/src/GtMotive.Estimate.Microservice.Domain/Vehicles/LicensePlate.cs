using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    public sealed record LicensePlate
    {
        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidLicensePlateException();
            }

            this.Value = value.Trim().ToUpperInvariant();
        }

        public string Value { get; }

        public override string ToString() => this.Value;
    }
}
