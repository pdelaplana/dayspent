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
    public class StatusReportMappingProfile : MappingProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CommandResult<StatusReportCategory>, WebApiResult<StatusReportCategoryViewModel>>();
            Mapper.CreateMap<CommandResult<StatusReport>, WebApiResult<StatusReportViewModel>>();
            Mapper.CreateMap<CommandResult<StatusReportItem>, WebApiResult<StatusReportItemViewModel>>();
            Mapper.CreateMap<CommandResult<StatusReportItemTag>, WebApiResult<StatusReportItemTagViewModel>>();

            Mapper.CreateMap<StatusReportCategory, StatusReportCategoryViewModel>();
            Mapper.CreateMap<StatusReport, StatusReportViewModel>();
            Mapper.CreateMap<StatusReportItem, StatusReportItemViewModel>()
                .ForMember(dest => dest.ReportingUserFullName, opts => opts.ResolveUsing<CacheUserFullNameResolver>().FromMember(src => src.StatusReport.ReportingUserId))
                .ForMember(dest => dest.ReportingUserId, opts => opts.MapFrom(src => src.StatusReport.ReportingUserId))
                .ForMember(dest => dest.StatusReportCategoryCode, opts => opts.MapFrom(src => src.StatusReportCategory.Code))
                .ForMember(dest => dest.StatusReportCategoryDescription, opts => opts.MapFrom(src => src.StatusReportCategory.Description))
                .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.Tags.Select(t => t.Tag.Name).ToArray()));

            Mapper.CreateMap<StatusReportItemTag, StatusReportItemTagViewModel>()
               .ForMember(dest => dest.TagName, opts => opts.MapFrom(src => src.Tag.Name));
        }
        
    }
}