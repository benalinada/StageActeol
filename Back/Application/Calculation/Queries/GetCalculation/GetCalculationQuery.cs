

using Application.Messure.Queries.GetMessure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calculation.Queries.GetCalculation
{
    public class GetCalculationQuery : IRequest<GetCalculationResponse>
    {
        public Guid ServerId { get; set; }
        public string DBCubeName { get; set; }
    }
}