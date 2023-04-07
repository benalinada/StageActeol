using Application.Attribute.Queries.GetAttribute;
using Application.Columns.Queries.GetColumns;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Attributes.Queries.GetAttribute
{
    public class GetTablesHandler : IRequestHandler<GetAttributeQuery, GetAttributeResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;

        public GetTablesHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<GetAttributeResponse> Handle(GetAttributeQuery request, CancellationToken cancellationToken)
        {
            var server = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == request.ServerId);
            if (server == null)
            {
                throw new ArgumentException("Invalid server ID specified.");
            }
            string connectionString = server.ConnexionString.Replace("master", request.DBName);

            var dimensions = string.Join("','",request.Dimentions);
            var query = $"SELECT COLUMN_NAME  as name, TABLE_NAME as tableName FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME in ('{dimensions}');  ";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    var data = dataTable.AsEnumerable().Select(r => new { name= r.Field<string>("name"), table= r.Field<string>("tableName") });
                    return new GetAttributeResponse()
                    {
                        DataBaseName = request.DBName,
                        Attribute = data.Select(t => new AttributeDto() { Id = Guid.NewGuid(), Name = t.name,TableName=t.table })
                    };

                }
                
            }

           
        }
    }
}


