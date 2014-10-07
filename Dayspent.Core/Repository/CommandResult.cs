using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Repository
{
    public struct CommandContext
    {
        public DateTime ExecutionTime;
        public string Initiator;
    }

    
    public class CommandResult<T>
    {
        public string ResultCode { get; set; }
        public string ResultText { get; set; }

        public CommandContext CommandContext;

        public T Data { get; set; }

    }
}
