using System.Text.Json.Serialization;

namespace IntentManagementAPI.DTOs.Shared
{
    public class AttachmentDto
    {
        public string? Id { get; set; }
        public string? Href { get; set; }
        public string? Description { get; set; }
        public string? AttachmentType { get; set; }
        public string? Content { get; set; }
        public string? MimeType { get; set; }
        public string? Url { get; set; }
        public TimePeriodDto? ValidFor { get; set; }

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