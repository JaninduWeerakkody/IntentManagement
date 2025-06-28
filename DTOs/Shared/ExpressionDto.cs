using System.Text.Json.Serialization;
using IntentManagementAPI.DTOs;

namespace IntentManagementAPI.DTOs.Shared
{
    [JsonDerivedType(typeof(JsonLdExpressionDto), "JsonLdExpression")]
    [JsonDerivedType(typeof(TurtleExpressionDto), "TurtleExpression")]
    public abstract class ExpressionDto : BaseDto
    {
        public int Id { get; set; }
        public string? ExpressionValue { get; set; }
        public string? Iri { get; set; }

        public ExpressionDto()
        {
            Type = "Expression";
            BaseType = "Entity";
            SchemaLocation = "https://mycsp.com:8080/tmfapi/schema/Common/Expression.schema.json";
        }
    }

    public class JsonLdExpressionDto : ExpressionDto
    {
        public JsonLdExpressionDto()
        {
            Type = "JsonLdExpression";
            BaseType = "Entity";
            SchemaLocation = "https://mycsp.com:8080/tmfapi/schema/Common/JsonLdExpression.schema.json";
        }
    }

    public class TurtleExpressionDto : ExpressionDto
    {
        public TurtleExpressionDto()
        {
            Type = "TurtleExpression";
            BaseType = "Entity";
            SchemaLocation = "https://mycsp.com:8080/tmfapi/schema/Common/TurtleExpression.schema.json";
        }
    }
} 