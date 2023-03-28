using Application.Common.Interfaces;
using Application.Database.Queries;
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
    public class GetTableHandler : IRequestHandler<GetTableQuery, GetTableResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        public GetTableHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext= applicationDBContext;
        }
        async Task<GetTableResponse>  IRequestHandler<GetTableQuery, GetTableResponse>.Handle(GetTableQuery request, CancellationToken cancellationToken)
        {
            var basedb = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);
            if (basedb == null)
            {
                throw new ArgumentException();
            }
             
            string connectionString ="Data Source=DESKTOP-0159C82\\VE_SERVER ;Initial Catalog=ActeolDb; Integrated Security=true;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(connectionString);

            // Ouverture de la connexion
            connection.Open();

            // Récupération de la liste des tables
            DataTable schemaTable = connection.GetSchema("Tables");
            string[] tableNames = new string[schemaTable.Rows.Count];
            int i = 0;
            foreach (DataRow row in schemaTable.Rows)
            {
                tableNames[i++] = (string)row[2];
            }

            // Fermeture de la connexion
            connection.Close();

            return new GetTableResponse()
            {
                Tables = tableNames.Select(t => new Domain.Common.Models.Table() { Id = Guid.NewGuid(), Name = t })
            };
        }
    }
}
