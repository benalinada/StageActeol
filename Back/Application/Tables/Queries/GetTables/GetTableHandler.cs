using Application.Common.Interfaces;
using Application.Database.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tables.Queries.GetTables
{
    public class GetTableHandler : IRequestHandler<GetTableQuery, GetTableResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        public GetTableHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext= applicationDBContext;
        }
        async Task<GetTableResponse>  IRequestHandler<GetTableQuery, GetTableResponse>.Handle(GetTableQuery request, CancellationToken cancellationToken)
        {
            var basedb = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);
            if (basedb == null)
            {
                throw new ArgumentException();
            }
            var resultat = new List<string>() { "TB1", "TB2" };
            return new GetTableResponse()
            {
                Tables = resultat.Select(t => new Domain.Common.Models.Table() { Id = Guid.NewGuid(), Name = t })
            };
        }
    }
}
