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
        public string SourceServerEngineId { get; set; }
        public string TargetServerEngineId { get; set; }

        public string SourceServerAnalyseId { get; set; }
        public string[] TargetServerAnalyseId { get; set; }

        public string SoureceAnalyserDb { get; set; } // cube selectionne 
        public string TargetEngineDb { get; set; } // dw

        public string Provider { get; set; } = "msolap";
        public string CubeName { get; set; } = "SampleCube";
    }
}
