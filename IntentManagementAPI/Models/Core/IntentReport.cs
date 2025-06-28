using System.Text.Json.Serialization;
using IntentManagementAPI.Models.Supporting;

namespace IntentManagementAPI.Models.Core
{
    public class IntentReport
    {
        public int Id { get; set; }
        public string? Href { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public TimePeriod? ValidFor { get; set; }
        public Expression? Expression { get; set; }
        public Intent? Intent { get; set; }

        [JsonPropertyName("@type")]
        public string? Type { get; set; } = "IntentReport";

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; } = "Entity";

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; } = "https://schema.example.com/IntentReport";
    }
} 