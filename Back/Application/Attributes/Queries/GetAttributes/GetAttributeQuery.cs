using Application.Attribute.Queries.GetAttribute;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Attributes.Queries.GetAttribute
{
    public class GetAttributeQuery : IRequest<GetAttributeResponse>
    {
        public Guid ServerId { get; set; }
        public string   DBName { get; set; }
        public string  TableName { get; set; }
        public string[] Dimentions { get; set; }

    }
}



