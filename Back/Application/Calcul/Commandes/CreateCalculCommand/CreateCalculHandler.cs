using Application.Common.Interfaces;
using Application.Helper.OLAPCube;
using MediatR;
using Microsoft.AnalysisServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calcul.Commandes.CreateCalculCommand
{
    public  class CreateCalculHandler : IRequestHandler<CreateCalculCommand, Unit>
    {
        private readonly IApplicationDBContext _applicationDBContext;

        public CreateCalculHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<Unit> Handle(CreateCalculCommand request, CancellationToken cancellationToken)
        {

            var serverSourceAnylser = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == new Guid(request.SourceServerAnalyseId));

            CubeGenerator.Calculation( request.SourceServerAnalyseName , request.SoureceAnalyserDb, request.Provider, request.Mes1, request.Mes2, request.Opr, request.Namecalculation);

            return Unit.Value;
        }

    }
}
