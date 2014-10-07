using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.API
{
    public class CommandContext
    {
        public DateTime ExecutionTime;
        public string Initiator;
    }

    
    public class WebApiResult<T>
    {
        public string ResultCode { get; set; }
        public string ResultText { get; set; }

        public CommandContext CommandContext;

        public T Data { get; set; }
        
    }
}