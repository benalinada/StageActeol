using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calcul.Commandes.CreateCalculCommand
{
    public  class CreateCalculCommand : IRequest<Unit>
    {

        public string SourceServerAnalyseId { get; set; } // serveur selectionne 
        public string SourceServerAnalyseName{ get; set; } // serveur selectionne 
        public string SoureceAnalyserDb { get; set; } // dbcube selectionne 
        public string Mes1 { get; set; } // le premier mesure   
        public string Mes2 { get; set; } // le deuxième messure 
        public string Opr { get; set; } // l'opération
        public string Namecalculation { get; set; } // l'opération name
        public string Provider { get; set; } = "msolap";
        public string CubeName { get; set; } = "SampleCube";
    }
}
