using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using QuickForms.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.MappingProfiles;
public class SurveyResultsProfile : Profile
{
    public SurveyResultsProfile()
    {
        CreateMap<SurveyResultsDto, SurveyResults>()
            .ForMember(dst => dst.SurveyResult, opt =>
            {
                opt.MapFrom(src => BsonDocument.Parse(src.SurveyResult));
            });

        CreateMap<SurveyResults, SurveyResultsDto>()
            .ForMember(dst => dst.SurveyResult, opt =>
            {
                opt.MapFrom(src => src.SurveyResult.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.CanonicalExtendedJson }, default, default, default));
            });
    }
}
