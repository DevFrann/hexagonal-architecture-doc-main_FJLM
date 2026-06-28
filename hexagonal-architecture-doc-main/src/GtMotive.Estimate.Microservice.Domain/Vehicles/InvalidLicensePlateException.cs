namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    public class InvalidLicensePlateException : DomainException
    {
        public InvalidLicensePlateException()
            : base("License plate cannot be empty.")
        {
        }
    }
}
