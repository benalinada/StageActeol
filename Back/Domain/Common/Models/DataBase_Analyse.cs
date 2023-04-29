using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    internal class DataBase_Analyse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ServeurId { get; set; }
        public virtual ServerAnalyse SreverAnalyse { get; set; }
        public virtual ICollection<Table> Tables { get; set; }
    }
}
