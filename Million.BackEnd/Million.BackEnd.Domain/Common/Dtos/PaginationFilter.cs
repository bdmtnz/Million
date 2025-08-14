using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Domain.Common.Dtos
{
    public record PaginationFilter(int Limit, int Offset);
}
