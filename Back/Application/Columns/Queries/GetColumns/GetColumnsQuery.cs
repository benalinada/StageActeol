﻿using Application.Tables.Queries.GetTables;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Columns.Queries.GetColumns
{
    public class GetColumnsQuery : IRequest<GetColumnsResponse>
    {
        public Guid ServerId { get; set; }
        public string DBName { get;  set; }
        public string TableName { get; set; }
    }
}