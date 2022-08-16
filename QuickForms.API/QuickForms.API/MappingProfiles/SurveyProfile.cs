using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using QuickForms.API.Models;

namespace QuickForms.API.MappingProfiles;
public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<Survey, SurveyDto>()
            .ForMember(dst => dst.Content, opt =>
            {
                opt.MapFrom(src => src.Content.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.CanonicalExtendedJson }, default, default, default));
            });

        CreateMap<NewSurveyDto, Survey>()
            .ForMember(dst => dst.Content, opt =>
            {
                opt.MapFrom(src => BsonDocument.Parse(src.Content));
            });

        CreateMap<UpdateSurveyDto, Survey>()
            .ForMember(dst => dst.Content, opt =>
            {
                opt.MapFrom(src => BsonDocument.Parse(src.Content));
            });
    }
}
