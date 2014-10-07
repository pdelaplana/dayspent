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
    public class ActivityMappingProfile : MappingProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CommandResult<Activity>, WebApiResult<ActivityViewModel>>();
            Mapper.CreateMap<CommandResult<ActivityTag>, WebApiResult<ActivityTagViewModel>>();


            Mapper.CreateMap<ActivityTag, ActivityTagViewModel>()
                .ForMember(dest => dest.TagName, opts => opts.MapFrom(src => src.Tag.Name));

            Mapper.CreateMap<Activity, ActivityViewModel>()
                .ForMember(dest => dest.ActivityByUserFullName, opts => opts.ResolveUsing<CacheUserFullNameResolver>().FromMember(src => src.ActivityByUserId))
                .ForMember(dest => dest.DateGroup, opts => opts.MapFrom(src => src.StartDate.Date))
                .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.Tags.Select(t=>t.Tag.Name).ToArray()))
                .ForMember(dest => dest.TagGroup, opts => opts.MapFrom(src => String.Join(",", src.Tags.OrderBy(t => t.Tag.Name).Select(t => t.Tag.Name).ToArray())));

        }
        
    }
}