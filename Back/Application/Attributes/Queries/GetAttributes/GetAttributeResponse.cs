using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Attribute.Queries.GetAttribute
{
    public class GetAttributeResponse
    {
        public string DataBaseName { get; set; }
        public IEnumerable<AttributeDto> Attribute { get; set; }
    }
    public class AttributeDto
    {
        public Guid Id { get; set; }
        public string TableName { get; set; }
        public string Name { get; internal set; }
    }
}
