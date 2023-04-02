using Application.Columns.Queries.GetColumns;
using Application.Common.Interfaces;
using Application.Database.Queries;
using Domain.Entities;
using MediatR;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tables.Queries.GetColumns
{
    public class GetColumnsHandler : IRequestHandler<GetColumnsQuery, GetColumnsResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;

        public GetColumnsHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<GetColumnsResponse> Handle(GetColumnsQuery request, CancellationToken cancellationToken)
        {
            // Récupérer le serveur correspondant à l'ID spécifié
            var server = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);

            if (server == null)
            {
                throw new ArgumentException("Invalid server ID specified.");
            }
            string connectionString = server.ConnexionString.Replace("master", request.DBName);
            var query = $"SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('{request.TableName}') ";
            //// Ouvrir une connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    string[] queryResponse = dataTable.AsEnumerable().Select(r => r.Field<string>("name")).ToArray();

                    // Utiliser le tableau de noms de base de données récupérés
                    return new GetColumnsResponse()
                    {
                        Columns = queryResponse.Select(t => new Domain.Common.Models.Column() { Id = Guid.NewGuid(), Name = t })
                    };
                }
            }
        }
        
    }
}


