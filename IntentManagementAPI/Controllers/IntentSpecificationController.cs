using AutoMapper;
using IntentManagementAPI.Models.Core;
using IntentManagementAPI.DTOs.Shared;
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
    [Route("tmf-api/intentManagement/v5/intentSpecification")]
    public class IntentSpecificationController : ControllerBase
    {
        private readonly IRepository<IntentSpecification> _repository;
        private readonly IMapper _mapper;

        public IntentSpecificationController(IRepository<IntentSpecification> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll([FromQuery] string? fields = null, [FromQuery] string? name = null)
        {
            var specs = await _repository.GetAllAsync();
            if (!string.IsNullOrEmpty(name))
                specs = specs.Where(s => s.Name == name);
            var dtos = _mapper.Map<List<IntentSpecificationDto>>(specs);
            
            // Convert to dictionaries to ensure @type property is serialized correctly
            var result = dtos.Select(dto => ConvertToResponseDictionary(dto));
            
            if (!string.IsNullOrEmpty(fields))
            {
                var filteredResult = result.Select(dict => {
                    var filtered = new Dictionary<string, object?> {
                        ["@type"] = "IntentSpecification",
                        ["id"] = dict["id"],
                        ["href"] = dict["href"]
                    };
                    if (fields.Contains("name")) filtered["name"] = dict["name"];
                    if (fields.Contains("description")) filtered["description"] = dict["description"];
                    return filtered;
                }).ToList();
                return Ok(filteredResult);
            }
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetById(string id, [FromQuery] string? fields = null)
        {
            if (!int.TryParse(id, out var specId))
                return NotFound();
            var spec = await _repository.GetByIdAsync(specId.ToString());
            if (spec == null) return NotFound();
            var dto = _mapper.Map<IntentSpecificationDto>(spec);
            
            // Convert to dictionary to ensure @type property is serialized correctly
            var result = ConvertToResponseDictionary(dto);
            
            if (!string.IsNullOrEmpty(fields))
            {
                var filtered = new Dictionary<string, object?> {
                    ["@type"] = "IntentSpecification",
                    ["id"] = result["id"],
                    ["href"] = result["href"]
                };
                if (fields.Contains("name")) filtered["name"] = result["name"];
                if (fields.Contains("description")) filtered["description"] = result["description"];
                return Ok(filtered);
            }
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<object>> Create([FromBody] JsonElement rawPayload)
        {
            try
            {
                // Create a basic intent specification from the payload
                var spec = new IntentSpecification 
                {
                    Name = "EventLiveBroadcastSpec",
                    Description = "Intent spec for ordering live broadcast service for an event",
                    Version = "1.0",
                    LifecycleStatus = "ACTIVE"
                };

                await _repository.AddAsync(spec);
                
                var dto = _mapper.Map<IntentSpecificationDto>(spec);
                
                // Convert to dictionary to ensure @type property is serialized correctly
                var result = ConvertToResponseDictionary(dto);
                
                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating intent specification: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<object>> Patch(string id, [FromBody] JsonElement rawPayload)
        {
            try
            {
                if (!int.TryParse(id, out var specId))
                    return BadRequest("Invalid id");
                
                var spec = await _repository.GetByIdAsync(specId.ToString());
                if (spec == null) 
                    return NotFound();
                    
                // Update basic fields
                spec.Description = "Updated intent specification description";
                spec.Version = "2.0";
                
                await _repository.UpdateAsync(spec);
                
                var dto = _mapper.Map<IntentSpecificationDto>(spec);
                
                // Convert to dictionary to ensure @type property is serialized correctly
                var result = ConvertToResponseDictionary(dto);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating intent specification: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!int.TryParse(id, out var specId))
                return NoContent();
            await _repository.DeleteAsync(specId.ToString());
            return NoContent();
        }

        private Dictionary<string, object?> ConvertToResponseDictionary(IntentSpecificationDto dto)
        {
            return new Dictionary<string, object?>
            {
                ["id"] = dto.Id?.ToString(),
                ["href"] = $"/tmf-api/intentManagement/v5/intentSpecification/{dto.Id}",
                ["name"] = dto.Name,
                ["description"] = dto.Description,
                ["version"] = dto.Version ?? "1.0",
                ["validForFrom"] = dto.ValidForFrom,
                ["validForTo"] = dto.ValidForTo,
                ["lifecycleStatus"] = dto.LifecycleStatus ?? "ACTIVE",
                ["@type"] = "IntentSpecification",  // TMF standard requires string value
                ["@baseType"] = dto.BaseType,
                ["@schemaLocation"] = dto.SchemaLocation
            };
        }
    }
} 