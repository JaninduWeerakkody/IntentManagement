using System.Text.Json.Serialization;

namespace IntentManagementAPI.DTOs
{
    public abstract class BaseDto
    {
        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; }

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; }
    }
}
