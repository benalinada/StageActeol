using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class serveurdata
    {
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public Guid DatabaseId { get; set; }
    }
}
