using System.Text.Json.Serialization;

namespace IntentManagementAPI.Models.Supporting
{
    public class Attachment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Href { get; set; }
        public string? Description { get; set; }
        public string? AttachmentType { get; set; }
        public string? Content { get; set; }
        public string? MimeType { get; set; }
        public string? Url { get; set; }
        public TimePeriod? ValidFor { get; set; }
        
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