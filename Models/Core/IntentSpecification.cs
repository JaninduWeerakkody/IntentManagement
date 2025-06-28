using System.Text.Json.Serialization;

namespace IntentManagementAPI.Models.Core
{
    public class IntentSpecification
    {
        public int Id { get; set; }
        public string? Href { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Version { get; set; }
        public DateTime? ValidForFrom { get; set; }
        public DateTime? ValidForTo { get; set; }
        public string? LifecycleStatus { get; set; }

        [JsonPropertyName("@type")]
        public string? Type { get; set; } = "IntentSpecification";

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; } = "Entity";

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; } = "https://schema.example.com/IntentSpecification";
    }
} 