using System;
using System.Text.Json.Serialization;
using IntentManagementAPI.DTOs.Shared;
using System.ComponentModel.DataAnnotations;

namespace IntentManagementAPI.DTOs.IntentReport
{
    public class IntentReportDto
    {
        public int? Id { get; set; }
        public string? Href { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public TimePeriodDto? ValidFor { get; set; }
        public ExpressionDto? Expression { get; set; }
        public int? IntentId { get; set; }

        [JsonPropertyName("@type")]
        public string? Type { get; set; } = "IntentReport";

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; } = "Entity";

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; } = "https://schema.example.com/IntentReport";
    }

    public class IntentReportUpdateDto
    {
        [Required]
        public string Type { get; set; } = "IntentReport";
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? IntentId { get; set; }
    }
} 