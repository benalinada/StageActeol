using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AttributFact.Queries.AttributFact
{
    public class GetAttributFactResponse
    {
        public string FactTableName { get; set; }
        public IEnumerable<AttributFactDto> AttributFact { get; set; }
    }
    public class AttributFactDto
    {
        public Guid Id { get; set; }
        public string TableName { get; set; }
        public string Name { get; internal set; }
    }
}
