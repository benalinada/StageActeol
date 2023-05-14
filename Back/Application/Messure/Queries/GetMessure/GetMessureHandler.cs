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
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Buffers.Text;
using Domain.Common.Models;
using Application.Helper.OLAPCube;
using Microsoft.AnalysisServices;


namespace Application.Messure.Queries.GetMessure
{
    public class GetMessureHandler : IRequestHandler<GetMessureQuery, GetMessureResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        public GetMessureHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<GetMessureResponse> Handle(GetMessureQuery request, CancellationToken cancellationToken)
        {
            var basedb = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);
            if (basedb == null)
            {
                throw new ArgumentException();
            }
            var messures = GetMessure(basedb.ConnexionString, request.DBCubeName);

            return new GetMessureResponse()
            {
                Messure = messures.Select(m => new MesuureDto() { Name = m.Name, Id =  Guid.NewGuid()}),
                DbCubeName = request.DBCubeName
            };


        }

        private IEnumerable<MessureCube> GetMessure(string strConection,string cubeName)
        {
           return CubeGenerator.GetMessure("localhost",cubeName, "msolap");

        }

    }

    
}
