using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dispatch.Commandes
{
    public class DispatchLookUpDto
    {
        public string SourceServerName { get; set; }
        public string TargetServerName { get; set; }
        public string SoureceDb { get; set; }
        public string NameCube { get; set; }
    }
}
