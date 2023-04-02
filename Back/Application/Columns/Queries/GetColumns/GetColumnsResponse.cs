using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Columns.Queries.GetColumns
{
    public class GetColumnsResponse
    {
        public IEnumerable<Column> Columns { get; set; }
    }
}
