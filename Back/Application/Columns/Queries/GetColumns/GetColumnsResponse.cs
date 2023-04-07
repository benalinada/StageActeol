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
        public string FactTableName { get; set; }
        public IEnumerable<ColumnDto> Columns { get; set; }
    }
    public class ColumnDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
