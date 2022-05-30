using Fronius.Dtos;

namespace Fronius.Models {
    public class PhotovoltaicSystemDetail {
        public PhotovoltaicSystemDetail(PhotovoltaicSystemDetailDto detail, IReadOnlyCollection<Dtos.ServiceMessageDto> messages) {
            Id = detail.Id;
            Name = detail.Name;
            PeakPower = detail.PeakPower;
            EnergyProducedCurrentDay = detail.EnergyProducedCurrentDay;
            InstallationDate = detail.InstallationDate;
            Owner = detail.Owner;
            Address = new Address(detail.Address);
            SystemCondition = BuildCondition(messages);
        }

        private Condition BuildCondition(IReadOnlyCollection<ServiceMessageDto> messages) {
            ServiceMessageLevel? levelWithMostMessages = messages.GroupBy(x => x.MessageLogLevel)
                .Select(g => new { Level = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                // Level: Error = 2, Warning = 1, Info = 0
                .ThenByDescending(x => x.Level)
                .Select(x => x.Level)
                .FirstOrDefault();

            return levelWithMostMessages switch {
                ServiceMessageLevel.Error => Condition.Error,
                ServiceMessageLevel.Warning => Condition.Warning,
                ServiceMessageLevel.Info => Condition.Good,
                null => Condition.Good,
                _ => throw new ArgumentException("ServiceMessageLevel enum value not handled")
            };
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public double PeakPower { get; set; }

        public double EnergyProducedCurrentDay { get; set; }


        public DateTimeOffset InstallationDate { get; set; }

        public string Owner { get; set; } = null!;

        public Address Address { get; set; } = null!;

        public Condition SystemCondition { get; }
    }
}
