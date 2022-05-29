using Fronius.Dtos;
using Fronius.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FroniusTests {
    public class PhotovoltaicSystemDetailTests {
        private PhotovoltaicSystemDetailDto dto;
        private PhotovoltaicSystemDetail? fixture;

        [SetUp]
        public void Setup() {
            dto = new PhotovoltaicSystemDetailDto {
                Id = Guid.NewGuid(),
                Name = "test",
                Owner = "irina test",
                PeakPower = 3.45645,
                EnergyProducedCurrentDay = 0.4535435,
                InstallationDate = DateTime.Now,
                Address = new AddressDto {
                    Street = "street",
                    ZipCode = "9090",
                    CountryCode = "AT",
                    CountryName = "Austria",
                    Latitude = 4.5645643,
                    Longitude = 7.34324324
                }
            };
        }

        [Test]
        public void ReturnsGoodConditionForNoMessages() {
            var emptyMessages = new List<ServiceMessageDto>();
            fixture = new PhotovoltaicSystemDetail(dto, emptyMessages);

            Assert.AreEqual(Condition.Good, fixture.SystemCondition);
        }

        [TestCase(3, 2, 2, Condition.Good)]
        [TestCase(0, 3, 2, Condition.Warning)]
        [TestCase(1, 3, 4, Condition.Error)]
        [TestCase(3, 3, 3, Condition.Error)]
        [TestCase(3, 3, 2, Condition.Warning)]
        public void ReturnsGoodConditionForMoreInfoMessages(int infoCount, int warningCount, int errorCount, Condition expectedCondition) {
            var emptyMessages = BuildMessages(infoCount, warningCount, errorCount);
            fixture = new PhotovoltaicSystemDetail(dto, emptyMessages);

            Assert.AreEqual(expectedCondition, fixture.SystemCondition);
        }

        private static List<ServiceMessageDto> BuildMessages(int infoCount, int warningCount, int errorCount) {
            var result = new List<ServiceMessageDto>();
            infoCount.Repeat(() => result.Add(new ServiceMessageDto { MessageLogLevel = ServiceMessageLevel.Info }));
            warningCount.Repeat(() => result.Add(new ServiceMessageDto { MessageLogLevel = ServiceMessageLevel.Warning }));
            errorCount.Repeat(() => result.Add(new ServiceMessageDto { MessageLogLevel = ServiceMessageLevel.Error }));

            // not an efficient way to shuffle
            var rng = new Random();
            return result.OrderBy(a => rng.Next()).ToList();
        }
    }
}