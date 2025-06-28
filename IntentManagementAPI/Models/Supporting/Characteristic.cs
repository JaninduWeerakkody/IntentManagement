using System.Text.Json.Serialization;

namespace IntentManagementAPI.Models.Supporting
{
    public class Characteristic
    {
        public string? Name { get; set; }
        public object? Value { get; set; }
        
        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; }

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; }

        public int Id { get; set; }
    }
} 