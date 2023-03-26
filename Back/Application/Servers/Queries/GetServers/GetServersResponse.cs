using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Servers.Qeuries.GetServers
{
    public class GetServersResponse
    {
        public IEnumerable<ServerDto> Servers { get; set; }
    }
}
