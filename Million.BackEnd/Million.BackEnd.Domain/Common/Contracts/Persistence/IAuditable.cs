using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface IAuditable
    {
        DateTime CreatedOnUtc { get; }
        DateTime? UpdatedOnUtc { get; }
    }
}
