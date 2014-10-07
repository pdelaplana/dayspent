using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dayspent.Security;
using Dayspent.Web.Application;
using Dayspent.Web.Models;

namespace Dayspent.Web.Application.Configuration.Automapper.Profiles
{
    public class UserProfileMappingProfile : MappingProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ApplicationUser, AuthenticatedUser>();

            Mapper.CreateMap<ApplicationUser, ProfileViewModel>()
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src=>src.Id));
           
        }
        
    }
}