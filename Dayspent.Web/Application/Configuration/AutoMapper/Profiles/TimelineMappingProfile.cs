using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Web.API;
using Dayspent.Web.Models;

namespace Dayspent.Web.Application.Configuration.Automapper.Profiles
{
    public class TimelineMappingProfile : MappingProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CommandResult<Timeline>, WebApiResult<TimelineViewModel>>();

            Mapper.CreateMap<Timeline, TimelineViewModel>();
                //.ForMember(dest => dest.Activities, opts => opts.MapFrom(src => src.Activities.OrderByDescending(a=>a.StartDate).Take(100).ToList()));

        }
        
    }
}