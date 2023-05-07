using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cube.Commandes.CreateCubeCommand
{
    public  class CreateCubeCommand : IRequest<Unit>
    {

        public string? DBEngineServer ;  
        public string? DBAnalyserServer ;
        public string? DBAnalyserServerName ;
        public string ProviderName ="msolap";
        public string? FactTableName ;
        public string? DBName ;
        public string? CubeDBName ;
        public string CubeDataSourceName = "Data";
        public string CubeDataSourceViewName = "DataView";
        public int DimensionTableCount = 1;
        public bool EmptyCube { get; set; } = false;

        public string[,] TableNamesAndKeys;
    }
}
