using System.Text.Json.Serialization;

namespace IntentManagementAPI.Models.Core
{
    [JsonDerivedType(typeof(JsonLdExpression), "JsonLdExpression")]
    [JsonDerivedType(typeof(TurtleExpression), "TurtleExpression")]
    public abstract class Expression
    {
        public int Id { get; set; }
        public string? ExpressionValue { get; set; }
        public string? Iri { get; set; }

        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        [JsonPropertyName("@schemaLocation")]
        public string? SchemaLocation { get; set; }
    }
} 