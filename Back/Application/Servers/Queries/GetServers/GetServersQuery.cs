using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Servers.Qeuries.GetServers
{
    public class GetServersQuery : IRequest<GetServersResponse>
    {
        public Guid UserId { get; set; }
    }
}
