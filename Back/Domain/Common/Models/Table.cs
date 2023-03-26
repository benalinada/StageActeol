using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    public class Table
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DatabaseId { get; set; }
        public DataBase Database { get; set; }
        public ICollection<Column> Columns { get; set; }

    }
}
