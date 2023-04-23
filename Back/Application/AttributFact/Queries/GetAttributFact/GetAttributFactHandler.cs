using Application.Attribute.Queries.GetAttribute;
using Application.AttributFact.Queries.AttributFact;
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AttributFact.Queries.GetAttributFact
{
    public class GetAttributFactHandler : IRequestHandler<GetAttributFactQuery, GetAttributFactResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;

        public GetAttributFactHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<GetAttributFactResponse> Handle(GetAttributFactQuery request, CancellationToken cancellationToken)
        {
            // Récupérer le serveur correspondant à l'ID spécifié
            var server = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);

            if (server == null)
            {
                throw new ArgumentException("Invalid server ID specified.");
            }
            string connectionString = server.ConnexionString.Replace("master", request.DBName);
            var query = $"SELECT column_name as name, TABLE_NAME as tableName FROM information_schema.columns WHERE table_name = '{request.TableName}';";
            //// Ouvrir une connexion à la base de données
      
        
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);



                    var data = dataTable.AsEnumerable().Select(r => new { name = r.Field<string>("name"), table = r.Field<string>("tableName") });

                    // Utiliser le tableau de noms de base de données récupérés
                    return new GetAttributFactResponse()
                    {
                        FactTableName = request.TableName,
                        AttributFact = data.Select(t => new AttributFactDto() { Id = Guid.NewGuid(), Name = t.name, TableName = t.table })
                        
                    };
                }
            }
        }
        
    }
}


