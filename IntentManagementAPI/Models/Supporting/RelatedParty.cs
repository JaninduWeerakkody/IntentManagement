using System.Text.Json.Serialization;

namespace IntentManagementAPI.Models.Supporting
{
    public class RelatedParty
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Href { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }

        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; }

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; }

        [JsonPropertyName("@referredType")]
        public string? ReferredType { get; set; }
    }
} 