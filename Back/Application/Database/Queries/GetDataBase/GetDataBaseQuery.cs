
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Database.Queries.GetDataBase
{
    public class GetDataBaseQuery : IRequest<GetDataBaseResponse>
    {
        public Guid ServerId { get; set; }
    }
}
