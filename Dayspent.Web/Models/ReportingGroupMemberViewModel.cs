using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class ReportingGroupMemberViewModel
    {
        public int ReportingGroupMemberId { get; set; }
        public int ReportingGroupId { get; set; }
        public string MemberUserId { get; set; }
        public string MemberFullName { get; set; }
    }
}