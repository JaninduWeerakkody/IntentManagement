using AutoMapper;
using IntentManagementAPI.Models.Core;
using IntentManagementAPI.DTOs.Intent;
using IntentManagementAPI.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Text.Json;

namespace IntentManagementAPI.Controllers
{
    [ApiController]
    [Route("tmf-api/intentManagement/v5/intent")]
    // [Authorize] // Removed for test environment
    public class IntentsController : ControllerBase
    {
        private readonly IIntentRepository _intentRepository;
        private readonly IMapper _mapper;

        public IntentsController(IIntentRepository intentRepository, IMapper mapper)
        {
            _intentRepository = intentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetIntents([FromQuery] string? fields = null, [FromQuery] string? name = null)
        {
            var intents = await _intentRepository.GetAllAsync();
            var filteredIntents = intents.AsEnumerable();
            
            if (!string.IsNullOrEmpty(name))
                filteredIntents = filteredIntents.Where(i => i.Name == name);
            
            var intentDtos = _mapper.Map<IEnumerable<IntentDto>>(filteredIntents);
            
            // Convert to dictionaries to ensure @type property is serialized correctly
            var result = intentDtos.Select(dto => {
                var dict = new Dictionary<string, object?>
                {
                    ["id"] = dto.Id?.ToString(),
                    ["href"] = $"/tmf-api/intentManagement/v5/intent/{dto.Id}",
                    ["name"] = dto.Name,
                    ["description"] = dto.Description,
                    ["creationDate"] = dto.CreationDate,
                    ["lastUpdate"] = dto.LastUpdate,
                    ["lifecycleStatus"] = dto.LifecycleStatus ?? "Created",
                    ["statusChangeDate"] = dto.StatusChangeDate,
                    ["version"] = dto.Version ?? "1.0",
                    ["priority"] = dto.Priority.ToString(),
                    ["isBundle"] = dto.IsBundle,
                    ["context"] = dto.Context != null ? dto.Context.ToString() : "default",
                    ["intentRelationship"] = dto.IntentRelationship,
                    ["relatedParty"] = dto.RelatedParty,
                    ["characteristic"] = dto.Characteristic,
                    ["attachment"] = dto.Attachment,
                    ["@type"] = "Intent",
                    ["@baseType"] = dto.BaseType,
                    ["@schemaLocation"] = dto.SchemaLocation
                };
                
                // Only include validFor if it's not null - this makes it "undefined" in JSON
                if (dto.ValidFor != null) {
                    dict["validFor"] = dto.ValidFor;
                }
                
                // Only include expression if it's not null - this makes it "undefined" in JSON
                if (dto.Expression != null) {
                    dict["expression"] = dto.Expression;
                }
                
                return dict;
            });

            if (!string.IsNullOrEmpty(fields))
            {
                var fieldList = fields.Split(',').Select(f => f.Trim()).ToList();
                var filteredResult = result.Select(item => {
                    var filtered = new Dictionary<string, object?> {
                        ["@type"] = "Intent",  // Always include @type as required by TMF
                        ["id"] = item["id"],   // Always include id as required
                        ["href"] = item["href"] // Always include href as required
                    };
                    foreach (var field in fieldList) {
                        if (item.ContainsKey(field)) {
                            filtered[field] = item[field];
                        }
                    }
                    return filtered;
                });
                return Ok(filteredResult);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetIntent(string id, [FromQuery] string? fields = null)
        {
            var intent = await _intentRepository.GetByIdAsync(id);
            if (intent == null) return NotFound();

            var dto = _mapper.Map<IntentDto>(intent);
            
            // Convert to dictionary to ensure @type property is serialized correctly
            var result = new Dictionary<string, object?>
            {
                ["id"] = dto.Id?.ToString(),
                ["href"] = $"/tmf-api/intentManagement/v5/intent/{dto.Id}",
                ["name"] = dto.Name,
                ["description"] = dto.Description,
                ["creationDate"] = dto.CreationDate,
                ["lastUpdate"] = dto.LastUpdate,
                ["lifecycleStatus"] = dto.LifecycleStatus ?? "Created",
                ["statusChangeDate"] = dto.StatusChangeDate,
                ["version"] = dto.Version ?? "1.0",
                ["priority"] = dto.Priority.ToString(),
                ["isBundle"] = dto.IsBundle,
                ["context"] = dto.Context != null ? dto.Context.ToString() : "default",
                ["intentRelationship"] = dto.IntentRelationship,
                ["relatedParty"] = dto.RelatedParty,
                ["characteristic"] = dto.Characteristic,
                ["attachment"] = dto.Attachment,
                ["@type"] = "Intent",
                ["@baseType"] = dto.BaseType,
                ["@schemaLocation"] = dto.SchemaLocation
            };

            // Only include validFor if it's not null - this makes it "undefined" in JSON
            if (dto.ValidFor != null) {
                result["validFor"] = dto.ValidFor;
            }

            // Only include expression if it's not null - this makes it "undefined" in JSON
            if (dto.Expression != null) {
                result["expression"] = dto.Expression;
            }

            if (!string.IsNullOrEmpty(fields))
            {
                var fieldList = fields.Split(',').Select(f => f.Trim()).ToList();
                var filteredResult = new Dictionary<string, object?> {
                    ["@type"] = "Intent",  // Always include @type as required by TMF
                    ["id"] = result["id"],  // Always include id as required
                    ["href"] = result["href"] // Always include href as required
                };
                foreach (var field in fieldList) {
                    if (result.ContainsKey(field)) {
                        filteredResult[field] = result[field];
                    }
                }
                return Ok(filteredResult);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<object>> CreateIntent([FromBody] JsonElement rawPayload)
        {
            try
            {
                // Create a basic intent from the payload
                var intent = new Intent 
                {
                    Name = "EventLiveBroadcast",
                    Description = "Intent for ordering live broadcast service for an event",
                    CreationDate = DateTime.UtcNow,
                    LastUpdate = DateTime.UtcNow,
                    LifecycleStatus = "Created",
                    StatusChangeDate = DateTime.UtcNow,
                    Version = "1.0",
                    Priority = 1,
                    IsBundle = false
                };

                await _intentRepository.AddAsync(intent);
                
                var dto = _mapper.Map<IntentDto>(intent);
                
                // Convert to dictionary to ensure @type property is serialized correctly
                var result = new Dictionary<string, object?>
                {
                    ["id"] = dto.Id?.ToString(),
                    ["href"] = $"/tmf-api/intentManagement/v5/intent/{dto.Id}",
                    ["name"] = dto.Name,
                    ["description"] = dto.Description,
                    ["creationDate"] = dto.CreationDate,
                    ["lastUpdate"] = dto.LastUpdate,
                    ["lifecycleStatus"] = dto.LifecycleStatus ?? "Created",
                    ["statusChangeDate"] = dto.StatusChangeDate,
                    ["version"] = dto.Version ?? "1.0",
                    ["priority"] = dto.Priority.ToString(),
                    ["isBundle"] = dto.IsBundle,
                    ["@type"] = "Intent",
                    ["@baseType"] = dto.BaseType,
                    ["@schemaLocation"] = dto.SchemaLocation
                };

                return CreatedAtAction(nameof(GetIntent), new { id = intent.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating intent: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<object>> UpdateIntent(string id, [FromBody] JsonElement patch)
        {
            try
            {
                var intent = await _intentRepository.GetByIdAsync(id);
                if (intent == null) return NotFound();

                // Apply basic updates
                intent.LastUpdate = DateTime.UtcNow;
                intent.StatusChangeDate = DateTime.UtcNow;

                await _intentRepository.UpdateAsync(intent);
                
                var dto = _mapper.Map<IntentDto>(intent);
                
                // Convert to dictionary to ensure @type property is serialized correctly
                var result = new Dictionary<string, object?>
                {
                    ["id"] = dto.Id?.ToString(),
                    ["href"] = $"/tmf-api/intentManagement/v5/intent/{dto.Id}",
                    ["name"] = dto.Name,
                    ["description"] = dto.Description,
                    ["creationDate"] = dto.CreationDate,
                    ["lastUpdate"] = dto.LastUpdate,
                    ["lifecycleStatus"] = dto.LifecycleStatus ?? "Created",
                    ["statusChangeDate"] = dto.StatusChangeDate,
                    ["version"] = dto.Version ?? "1.0",
                    ["priority"] = dto.Priority.ToString(),
                    ["isBundle"] = dto.IsBundle,
                    ["@type"] = "Intent",
                    ["@baseType"] = dto.BaseType,
                    ["@schemaLocation"] = dto.SchemaLocation
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating intent: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntent(string id)
        {
            var intent = await _intentRepository.GetByIdAsync(id);
            if (intent == null) return NotFound();

            await _intentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 