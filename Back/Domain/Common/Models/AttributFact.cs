using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    internal class AttributFact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TypeId { get; set; }
        public Guid TableId { get; set; }
        public virtual Table Table { get; set; }
        public virtual Type Type { get; set; }
    }
}

