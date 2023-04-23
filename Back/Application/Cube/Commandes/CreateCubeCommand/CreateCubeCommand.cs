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

        public string DBServer ;
        public string ProviderName ;
        public string FactTableName ;
        public string DBName ;
        public string CubeDBName ;
        public string CubeDataSourceName ;
        public string CubeDataSourceViewName ;
        public int DimensionTableCount = 1;

        public string[,] TableNamesAndKeys;
    }
}
