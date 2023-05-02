using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dispatch.Commandes
{
    public class CreateDisptachCommand : IRequest<CreateDispatchResponse>
    {
        public Guid SourceServerId { get; set; }
        public string[] TargetServerId { get; set; }
        public string SoureceDb { get; set; }
        public string NameCube { get; set; }
    }
}
