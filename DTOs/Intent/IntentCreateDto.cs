using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IntentManagementAPI.DTOs.Intent
{
    public class IntentCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? BaseType { get; set; } = "Intent";
        [JsonPropertyName("@type")]
        public string Type { get; set; } = "Intent";
    }
} 