using System.Text.Json.Serialization;
using IntentManagementAPI.Models.Core;

namespace IntentManagementAPI.Models.Supporting
{
    public class IntentRelationship
    {
        public int Id { get; set; }
        public string? Href { get; set; }
        public string? RelationshipType { get; set; }
        
        // Reference to the related intent
        public Intent? Intent { get; set; }
        public int? IntentId { get; set; }

        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; }

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; }
    }
} 