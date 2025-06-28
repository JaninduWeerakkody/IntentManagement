using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace IntentManagementAPI.Models.Supporting
{
    public class Context
    {
        [JsonPropertyName("@type")]
        public string? Type { get; set; } = "Context";

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; } = "Entity";

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; } = "https://mycsp.com:8080/tmfapi/schema/Common/Context.schema.json";
        
        public int Id { get; set; }
        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
        public string? LifecycleStatus { get; set; }
        public DateTime StatusChangeDate { get; set; }
        public string? Version { get; set; }
    }
} 