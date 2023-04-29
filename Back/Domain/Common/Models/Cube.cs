using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    internal class Cube
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid Database_AnalyseId { get; set; }
        public DataBase_Analyse Database_Analyse { get; set; }
    }
}
