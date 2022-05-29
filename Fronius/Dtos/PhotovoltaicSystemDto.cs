
namespace Fronius.Dtos {
    public class PhotovoltaicSystemDto {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public double PeakPower { get; set; }

        public double EnergyProducedCurrentDay { get; set; }
    }
}
