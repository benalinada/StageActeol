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


namespace Application.Calculation.Queries.GetCalculation
{
    public class GetCalculationHandler : IRequestHandler<GetCalculationQuery, GetCalculationResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        public GetCalculationHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<GetCalculationResponse> Handle(GetCalculationQuery request, CancellationToken cancellationToken)
        {
            var basedb = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);
            if (basedb == null)
            {
                throw new ArgumentException();
            }
            var Calculation = GetCalculation(basedb.ConnexionString, request.DBCubeName);

            return new GetCalculationResponse()
            {
                Calculation = Calculation.Select(m => new CalculationeDto() { Name = m.Name, Id =  Guid.NewGuid()}),
                DbCubeName = request.DBCubeName
            };


        }

        private IEnumerable<CalculationCube> GetCalculation(string strConection,string cubeName)
        {
           return CubeGenerator.GetCalculation("localhost",cubeName, "msolap");

        }


    }

    
}
