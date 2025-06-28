using AutoMapper;
using IntentManagementAPI.DTOs.Intent;
using IntentManagementAPI.DTOs.Shared;
using IntentManagementAPI.Models.Core;
using IntentManagementAPI.Models.Supporting;

namespace IntentManagementAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Model to DTO
            CreateMap<TimePeriod, TimePeriodDto>();
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<Characteristic, CharacteristicDto>();
            CreateMap<Context, ContextDto>();
            CreateMap<IntentRelationship, IntentRelationshipDto>();
            CreateMap<RelatedParty, RelatedPartyDto>();
            CreateMap<Expression, ExpressionDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Expression"))
                .Include<JsonLdExpression, JsonLdExpressionDto>()
                .Include<TurtleExpression, TurtleExpressionDto>();
            CreateMap<JsonLdExpression, JsonLdExpressionDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "JsonLdExpression"));
            CreateMap<TurtleExpression, TurtleExpressionDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "TurtleExpression"));
            CreateMap<Intent, IntentDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Intent"))
                .ForMember(dest => dest.BaseType, opt => opt.MapFrom(src => "Entity"))
                .ForMember(dest => dest.SchemaLocation, opt => opt.MapFrom(src => "https://mycsp.com:8080/tmfapi/schema/Common/Intent.schema.json"));
            CreateMap<ProbeIntent, IntentDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "ProbeIntent"))
                .ForMember(dest => dest.BaseType, opt => opt.MapFrom(src => "Entity"))
                .ForMember(dest => dest.SchemaLocation, opt => opt.MapFrom(src => "https://mycsp.com:8080/tmfapi/schema/Common/Intent.schema.json"));
            CreateMap<Expression, ExpressionDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Expression"))
                .ForMember(dest => dest.BaseType, opt => opt.MapFrom(src => "Entity"))
                .ForMember(dest => dest.SchemaLocation, opt => opt.MapFrom(src => "https://mycsp.com:8080/tmfapi/schema/Common/Expression.schema.json"))
                .Include<JsonLdExpression, JsonLdExpressionDto>()
                .Include<TurtleExpression, TurtleExpressionDto>();
            CreateMap<JsonLdExpression, JsonLdExpressionDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "JsonLdExpression"))
                .ForMember(dest => dest.BaseType, opt => opt.MapFrom(src => "Entity"))
                .ForMember(dest => dest.SchemaLocation, opt => opt.MapFrom(src => "https://mycsp.com:8080/tmfapi/schema/Common/JsonLdExpression.schema.json"));
            CreateMap<TurtleExpression, TurtleExpressionDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "TurtleExpression"))
                .ForMember(dest => dest.BaseType, opt => opt.MapFrom(src => "Entity"))
                .ForMember(dest => dest.SchemaLocation, opt => opt.MapFrom(src => "https://mycsp.com:8080/tmfapi/schema/Common/TurtleExpression.schema.json"));
            CreateMap<Intent, IntentDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Intent"))
                .ForMember(dest => dest.BaseType, opt => opt.MapFrom(src => "Entity"))
                .ForMember(dest => dest.SchemaLocation, opt => opt.MapFrom(src => "https://mycsp.com:8080/tmfapi/schema/Common/Intent.schema.json"));
            CreateMap<ProbeIntent, IntentDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "ProbeIntent"))
                .ForMember(dest => dest.BaseType, opt => opt.MapFrom(src => "Entity"))
                .ForMember(dest => dest.SchemaLocation, opt => opt.MapFrom(src => "https://mycsp.com:8080/tmfapi/schema/Common/Intent.schema.json"));
            CreateMap<IntentReport, DTOs.IntentReport.IntentReportDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "IntentReport"))
                .ForMember(dest => dest.IntentId, opt => opt.MapFrom(src => src.Intent != null ? (int?)src.Intent.Id : null));
            CreateMap<DTOs.IntentReport.IntentReportDto, IntentReport>();
            CreateMap<IntentSpecification, DTOs.Shared.IntentSpecificationDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "IntentSpecification"));
            CreateMap<DTOs.Shared.IntentSpecificationDto, IntentSpecification>();

            // DTO to Model
            CreateMap<TimePeriodDto, TimePeriod>();
            CreateMap<AttachmentDto, Attachment>();
            CreateMap<CharacteristicDto, Characteristic>();
            CreateMap<ContextDto, Context>();
            CreateMap<IntentRelationshipDto, IntentRelationship>();
            CreateMap<RelatedPartyDto, RelatedParty>();
            CreateMap<ExpressionDto, Expression>()
                .Include<JsonLdExpressionDto, JsonLdExpression>()
                .Include<TurtleExpressionDto, TurtleExpression>();
            CreateMap<JsonLdExpressionDto, JsonLdExpression>();
            CreateMap<TurtleExpressionDto, TurtleExpression>();
            CreateMap<IntentDto, Intent>();
            CreateMap<IntentCreateDto, Intent>();
            CreateMap<IntentUpdateDto, Intent>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
} 