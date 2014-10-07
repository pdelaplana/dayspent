using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dayspent.Core.Models;

namespace Dayspent.Core.Repository
{
    public interface IApplicationRepositoryCommand<T>
    {
        CommandResult<T> Execute(ApplicationDb db);
    }
}
