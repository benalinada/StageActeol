using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Common.Models
{
    public class DataBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ServeurId { get; set; }
        public virtual Server Srever { get; set; }
        public virtual ICollection<Table> Tables { get; set; }
    }
}
