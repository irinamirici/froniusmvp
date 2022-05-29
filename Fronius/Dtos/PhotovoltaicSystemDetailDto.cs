using System.Text.Json.Serialization;

namespace Fronius.Dtos {
    public class PhotovoltaicSystemDetailDto {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public double PeakPower { get; set; }

        [JsonPropertyName("energyProducedToday")]
        public double EnergyProducedCurrentDay { get; set; }

        public DateTimeOffset InstallationDate { get; set; }

        public string Owner { get; set; } = null!;

        public AddressDto Address { get; set; } = null!;
    }
}
