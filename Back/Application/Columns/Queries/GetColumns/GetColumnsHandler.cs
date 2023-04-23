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

namespace Application.Columns.Queries.GetColumns
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
            var query = $"SELECT d.name AS name FROM sys.foreign_keys fk INNER JOIN sys.tables f ON fk.parent_object_id = f.object_id INNER JOIN sys.tables d ON fk.referenced_object_id = d.object_id WHERE f.type = 'U' AND d.type = 'U' AND fk.referenced_object_id IS NOT NULL AND f.name = '{request.TableName}' GROUP BY d.name  ";
            //// Ouvrir une connexion à la base de données
            ///{request.TableName}
        
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
                        FactTableName = request.TableName,
                        Columns = queryResponse.Select(t => new ColumnDto() { Id = Guid.NewGuid(), Name = t, })
                    };
                }
            }
        }
        
    }
}


