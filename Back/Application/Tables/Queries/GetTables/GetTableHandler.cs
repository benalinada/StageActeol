using Application.Common.Interfaces;
using Application.Database.Queries;
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Récupérer la liste des tables de la base de données spécifiée
                DataTable schemaTable = connection.GetSchema("Tables");
                string[] tableNames = new string[schemaTable.Rows.Count];

                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    tableNames[i] = schemaTable.Rows[i].Field<string>("TABLE_NAME");
                }

                // Fermer la connexion
                connection.Close();

                //    // Retourner la liste des tables sous forme de réponse
                return new GetTablesResponse()
                {
                    Tables = tableNames.Select(t => new Domain.Common.Models.Table() { Id = Guid.NewGuid(), Name = t })
                };

            }
        }
    }
}


