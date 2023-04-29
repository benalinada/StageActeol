using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Servers.Qeuries.GetServers
{
    public class GetServersHandler : IRequestHandler<GetServersQuery, GetServersResponse>
    {
        private readonly IApplicationDBContext _dbContext;
        public GetServersHandler(IApplicationDBContext applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }
        public async Task<GetServersResponse> Handle(GetServersQuery request, CancellationToken cancellationToken)
        {
            var Server = _dbContext.UserServers.Where(us => us.UserId == request.UserId)
                                        .Select(us => _dbContext.Servers.SingleOrDefault(s => s.Id == us.ServerId)).ToList();

            return new GetServersResponse()
            {
                Servers = Server.Select(s => new ServerDto() { Id = s.Id, Name = s.Name , type=s.type })
            };

        }
    }
}
