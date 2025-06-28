using System.Text.Json.Serialization;

namespace IntentManagementAPI.DTOs.Shared
{
    public class ContextDto
    {
        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; }

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; }
        
        public string? Name { get; set; }
    }
} 