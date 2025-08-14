using Million.BackEnd.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Domain.Common.Contracts
{
    public interface IEntity<EId> where EId : ValueObject;
}
