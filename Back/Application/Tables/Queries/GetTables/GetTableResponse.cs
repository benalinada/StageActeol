using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tables.Queries.GetTables
{
    public class GetTablesResponse
    {
        public string DataBaseName { get; set; }
        public IEnumerable<TableDto> Tables { get; set; }
    }
    public class TableDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
