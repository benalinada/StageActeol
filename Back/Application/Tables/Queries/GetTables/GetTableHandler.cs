using Application.Common.Interfaces;
using Application.Database.Queries;
using Domain.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tables.Queries.GetTables
{
    public class GetTablesHandler : IRequestHandler<GetTablesQuery, GetTablesResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;

        public GetTablesHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<GetTablesResponse> Handle(GetTablesQuery request, CancellationToken cancellationToken)
        {
            // Récupérer le serveur correspondant à l'ID spécifié
            var server = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);

            if (server == null)
            {
                throw new ArgumentException("Invalid server ID specified.");
            }

            //// Construire la chaîne de connexion en utilisant les informations du serveur
            //string connectionString = $"Data Source={server.Name};Initial Catalog=ActeolDb ;Integrated Security=True;";
            string connectionString = server.ConnexionString.Replace("master", request.DBName);

            //// Ouvrir une connexion à la base de données
            var query = $"SELECT  f.name AS name FROM  sys.foreign_keys fk   INNER JOIN sys.tables f ON fk.parent_object_id = f.object_id    INNER JOIN sys.tables d ON fk.referenced_object_id = d.object_id WHERE     f.type = 'U'     AND d.type = 'U'  GROUP BY     f.name ";
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
                    return new GetTablesResponse()
                    {
                        DataBaseName = request.DBName,
                        Tables = queryResponse.Select(t => new TableDto() { Id = Guid.NewGuid(), Name = t, })
                    };
                }
            }
        }
    }
}


