using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Database.Queries
{
    public class GetDataBaseHandler : IRequestHandler<GetDataBaseQuery, GetDataBaseResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        public GetDataBaseHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async  Task<GetDataBaseResponse> Handle(GetDataBaseQuery request, CancellationToken cancellationToken)
        {
            var basedb = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);
            if (basedb == null)
            {
                throw new ArgumentException();
            }
            var resultat  = new List<string>() { "Db1","Db2"};
            return new GetDataBaseResponse()
            {
                DataBases = resultat.Select(t => new Domain.Common.Models.DataBase() {Id= Guid.NewGuid(), Name = t })
            };
        }
    }
}
