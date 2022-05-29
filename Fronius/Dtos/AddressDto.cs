using System.Text.Json.Serialization;

namespace Fronius.Dtos {
    public class AddressDto {
        public string Street { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string CountryName { get; set; } = null!;

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
    }
}
