using Fronius.Dtos;

namespace Fronius.Models {
    public class PhotovoltaicSystem {
        public PhotovoltaicSystem(PhotovoltaicSystemDetailDto pv) {
            Id = pv.Id;
            Name = pv.Name;
            PeakPower = pv.PeakPower;
            EnergyProducedCurrentDay = pv.EnergyProducedCurrentDay;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public double PeakPower { get; set; }

        public double EnergyProducedCurrentDay { get; set; }
    }
}
