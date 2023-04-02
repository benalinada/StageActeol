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


            
         
            string query = "SELECT name FROM sys.databases";

            using (SqlConnection connection = new SqlConnection(basedb.ConnexionString))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    string[] queryResponse = dataTable.AsEnumerable().Select(r => r.Field<string>("name")).ToArray();

                    // Utiliser le tableau de noms de base de données récupérés
                    return new GetDataBaseResponse()
                    {
                        DataBases = queryResponse.Select(t => new Domain.Common.Models.DataBase() { Id = Guid.NewGuid(), Name = t })
                    };
                }
            }


         
        }
    }
}
