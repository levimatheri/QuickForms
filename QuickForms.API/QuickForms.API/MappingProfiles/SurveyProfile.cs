using AutoMapper;
using MongoDB.Bson;
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
                opt.MapFrom(src => BsonSerializer.Deserialize<dynamic>(src.Content, default));
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
