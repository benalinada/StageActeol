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


namespace Application.Database.Queries.GetDataBase
{
    public class GetDataBaseHandler : IRequestHandler<GetDataBaseQuery, GetDataBaseResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        public GetDataBaseHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<GetDataBaseResponse> Handle(GetDataBaseQuery request, CancellationToken cancellationToken)
        {
            var basedb = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);
            if (basedb == null)
            {
                throw new ArgumentException();
            }

            return new GetDataBaseResponse()
            {
                DataBases = basedb.type== "Engine" ? GetDataBaseSql(basedb.ConnexionString): GetDataBaseAnalyser(basedb.ConnexionString)
            };


        }


        private IEnumerable<DataBase> GetDataBaseSql(string strConection)
        {
            string query = "SELECT name FROM sys.databases";

            using (SqlConnection connection = new SqlConnection(strConection))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    string[] queryResponse = dataTable.AsEnumerable().Select(r => r.Field<string>("name")).ToArray();

                    // Utiliser le tableau de noms de base de données récupérés
                    return queryResponse.Select(t => new Domain.Common.Models.DataBase() { Id = Guid.NewGuid(), Name = t });


                }
            }
        }

        private IEnumerable<DataBase> GetDataBaseAnalyser(string strConection)
        {
           return CubeGenerator.GetDataBaseAnalyser("localhost", "msolap");

        }

    }

    
}
