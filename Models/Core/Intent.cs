using System.Text.Json.Serialization;
using IntentManagementAPI.Models.Supporting;
using System.Collections.Generic;

namespace IntentManagementAPI.Models.Core
{
    public class Intent
    {
        public int Id { get; set; }
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
        public Context? Context { get; set; }
        public TimePeriod? ValidFor { get; set; }
        public Expression? Expression { get; set; }
        public ICollection<IntentRelationship> IntentRelationship { get; set; } = new List<IntentRelationship>();
        public ICollection<RelatedParty> RelatedParty { get; set; } = new List<RelatedParty>();
        public ICollection<Characteristic> Characteristic { get; set; } = new List<Characteristic>();
        public ICollection<Attachment> Attachment { get; set; } = new List<Attachment>();
        public IntentSpecification? IntentSpecification { get; set; }

        [JsonPropertyName("@type")]
        public string Type { get; set; } = "Intent";

        [JsonPropertyName("@baseType")]
        public string? BaseType { get; set; }

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; }
    }
} 