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
    public class ReportingGroupMappingProfile : MappingProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ReportingGroup, ReportingGroupViewModel>();
            Mapper.CreateMap<ReportingGroupMember, ReportingGroupMemberViewModel>()
                .ForMember(dest => dest.MemberFullName, opts => opts.ResolveUsing<CacheUserFullNameResolver>().FromMember(src => src.MemberUserId));
                
        }
        
    }
}