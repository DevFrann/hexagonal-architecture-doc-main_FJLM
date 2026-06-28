using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles
{
    public class ListAvailableVehiclesOutput : IUseCaseOutput
    {
        public ListAvailableVehiclesOutput(IReadOnlyCollection<AvailableVehicleDto> vehicles)
        {
            this.Vehicles = vehicles;
        }

        public IReadOnlyCollection<AvailableVehicleDto> Vehicles { get; }
    }
}
