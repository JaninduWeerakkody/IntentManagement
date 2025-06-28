using System.ComponentModel.DataAnnotations;

namespace IntentManagementAPI.DTOs.Intent
{
    public class IntentUpdateDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? LifecycleStatus { get; set; }
        
        public IntentUpdateDto()
        {
            Type = "Intent";
            BaseType = "Entity";
            SchemaLocation = "https://mycsp.com:8080/tmfapi/schema/Common/Intent.schema.json";
        }
    }
} 