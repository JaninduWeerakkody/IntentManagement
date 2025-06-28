using System.Text.Json.Serialization;

namespace IntentManagementAPI.DTOs.Shared
{
    public class RelatedPartyDto
    {
        public string? Id { get; set; }
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
