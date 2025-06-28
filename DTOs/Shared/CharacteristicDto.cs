using System.Text.Json.Serialization;

namespace IntentManagementAPI.DTOs.Shared
{
    public class CharacteristicDto
    {
        public string? Name { get; set; }
        public object? Value { get; set; }

        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; }

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; }
    }
} 