using QuickForms.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;

namespace QuickForms.Client.MappingProfiles;
public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        //CreateMap<Survey, SurveyDto>()
        //    .ForMember(dst => dst.Content, opt =>
        //    {
        //        opt.MapFrom(src => Convert.ToString((object)src.Content));
        //    });
    }
}
