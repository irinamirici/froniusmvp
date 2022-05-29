using System.Text.Json.Serialization;

namespace Fronius.Dtos {
    public class ServiceMessageDto {
        [JsonPropertyName("type")]
        public ServiceMessageLevel MessageLogLevel { get; set; }
    }
}