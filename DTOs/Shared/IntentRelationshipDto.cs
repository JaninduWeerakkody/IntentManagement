using System.Text.Json.Serialization;
using IntentManagementAPI.DTOs.Intent;

namespace IntentManagementAPI.DTOs.Shared
{
    public class IntentRelationshipDto
    {
        public string? Id { get; set; }
        public string? Href { get; set; }
        public string? RelationshipType { get; set; }
        public IntentDto? Intent { get; set; }

        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; }

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; }
    }
} 