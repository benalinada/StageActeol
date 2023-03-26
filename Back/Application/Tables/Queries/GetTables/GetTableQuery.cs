using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tables.Queries.GetTables
{
    public class GetTableQuery : IRequest<GetTableResponse>
    {
        public Guid ServerId { get; set; }
        public string   DBName { get; set; }
    }
}
