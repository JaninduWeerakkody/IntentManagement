using IntentManagementAPI.Data.Repositories;
using IntentManagementAPI.Models.Core;
using IntentManagementAPI.Models.Supporting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace IntentManagementAPI.Data
{
    public static class DataSeeder
    {
        public static async Task SeedData(IRepository<Intent> intentRepository)
        {
            // Check if any intents exist
            var intents = await intentRepository.GetAllAsync();
            if (intents.Any()) return;

            // Create test data
            var intent = new Intent
            {
                Name = "Test Intent",
                Description = "Test Intent Description",
                CreationDate = DateTime.UtcNow,
                LastUpdate = DateTime.UtcNow,
                LifecycleStatus = "Active",
                StatusChangeDate = DateTime.UtcNow,
                Priority = 1,
                IsBundle = false,
                Context = new Context(),
                ValidFor = new TimePeriod 
                { 
                    StartDateTime = DateTime.UtcNow,
                    EndDateTime = DateTime.UtcNow.AddYears(1)
                },
                Expression = new JsonLdExpression { ExpressionValue = "Test expression", Type = "JsonLdExpression", SchemaLocation = "https://mycsp.com:8080/tmfapi/schema/Common/JsonLdExpression.schema.json" },
                IntentRelationship = new List<IntentRelationship>(),
                Characteristic = new List<Characteristic>(),
                Attachment = new List<Attachment>()
            };

            await intentRepository.AddAsync(intent);
            await intentRepository.SaveChangesAsync();
        }
    }
}
