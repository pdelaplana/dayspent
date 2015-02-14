using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Web.API;
using Dayspent.Web.Application.Configuration.Automapper.Resolvers;
using Dayspent.Web.Models;

namespace Dayspent.Web.Application.Configuration.Automapper.Profiles
{
    public class TrackableMappingProfile : MappingProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CommandResult<Trackable>, WebApiResult<TrackableViewModel>>();

            Mapper.CreateMap<Trackable, TrackableViewModel>()
                .ForMember(dest => dest.TrackableType, opts => opts.MapFrom(src => src.TrackableType.ToString()));

            
        }
        
    }
}