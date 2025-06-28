using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using IntentManagementAPI.DTOs.Shared;
using System.ComponentModel.DataAnnotations;

namespace IntentManagementAPI.DTOs.Intent
{
    public class IntentDto : BaseDto
    {
        public string? Id { get; set; }
        public string? Href { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string? LifecycleStatus { get; set; }
        public DateTime StatusChangeDate { get; set; }
        public string? Version { get; set; }
        public int Priority { get; set; }
        public bool IsBundle { get; set; }
        public ContextDto? Context { get; set; }
        public TimePeriodDto? ValidFor { get; set; }
        public ExpressionDto? Expression { get; set; }
        public ICollection<IntentRelationshipDto> IntentRelationship { get; set; } = new List<IntentRelationshipDto>();
        public ICollection<RelatedPartyDto> RelatedParty { get; set; } = new List<RelatedPartyDto>();
        public ICollection<CharacteristicDto> Characteristic { get; set; } = new List<CharacteristicDto>();
        public ICollection<AttachmentDto> Attachment { get; set; } = new List<AttachmentDto>();

        // Override the Type property to ensure correct JSON serialization
        [JsonPropertyName("@type")]
        public new string? Type { get; set; }

        public IntentDto()
        {
            // Initialize base properties
            Type = "Intent";
            BaseType = "Entity";
            SchemaLocation = "https://mycsp.com:8080/tmfapi/schema/Common/Intent.schema.json";
        }
    }
}
