using Fronius.Dtos;

namespace Fronius.Models {
    public class Address {
        public Address(AddressDto address) {
            Street = address.Street;
            ZipCode = address.ZipCode;
            CountryCode = address.CountryCode;
            CountryName = address.CountryName;
            Latitude = address.Latitude;
            Longitude = address.Longitude;
        }

        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}