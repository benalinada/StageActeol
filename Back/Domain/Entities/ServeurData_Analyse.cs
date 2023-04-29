using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class ServeurData_Analyse
    {
        public Guid Id { get; set; }
        public Guid Server_AnalyseId { get; set; }
        public Guid Database_AnalyseId { get; set; }
    }
}
