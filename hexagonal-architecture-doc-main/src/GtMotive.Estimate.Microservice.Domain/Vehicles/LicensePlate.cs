namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle license plate value object.
    /// </summary>
    public sealed record LicensePlate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlate"/> class.
        /// </summary>
        /// <param name="value">License plate value.</param>
        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidLicensePlateException();
            }

            this.Value = value.Trim().ToUpperInvariant();
        }

        /// <summary>
        /// Gets the normalized license plate text.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Returns the normalized license plate text.
        /// </summary>
        /// <returns>License plate text.</returns>
        public override string ToString() => this.Value;
    }
}
