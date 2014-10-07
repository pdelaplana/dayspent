using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Dayspent.Core.Repository;
using Dayspent.Web.API;

namespace Dayspent.Web.Application.Configuration.Automapper.Profiles
{
    public class GeneralMappingProfile : MappingProfileBase
    {
        protected override void Configure()
        {
            
            Mapper.CreateMap<KeyValuePair<string, string>, SelectListItem>()
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Key))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.Value));

            Mapper.CreateMap<Dayspent.Core.Repository.CommandContext, Dayspent.Web.API.CommandContext>();

            Mapper.CreateMap<CommandResult<bool>, WebApiResult<bool>>();

            Mapper.CreateMap<CommandResult<string>, WebApiResult<string>>();
        }
    }
}