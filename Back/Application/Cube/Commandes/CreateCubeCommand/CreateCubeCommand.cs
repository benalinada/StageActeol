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

        public string DBServerName = "DESKTOP-0159C82\\VE_SERVER";
        public string ProviderName = "msolap";
        public string FactTableName = "Fact_Bookings";
        public string DBName ;
        public string CubeDBName ;
        public string CubeDataSourceName = "Data";
        public string CubeDataSourceViewName = "DataView";
        public int DimensionTableCount = 1;

        public string[,] TableNamesAndKeys;
    }
}
