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

namespace Application.Cube.Commandes.CreateCubeCommand
{
    public  class CreateCubeHandler : IRequestHandler<CreateCubeCommand, Unit>
    {
        private readonly IApplicationDBContext _applicationDBContext;

        public CreateCubeHandler(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<Unit> Handle(CreateCubeCommand request, CancellationToken cancellationToken)
        {
            if (!request.EmptyCube)
            {
                var server = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == new Guid(request.DBEngineServer));

                if (server == null)
                {
                    throw new ArgumentException("Invalid server ID specified.");
                }
                request.DBEngineServer = server.ConnexionString;

            }
           if(!string.IsNullOrEmpty(request.DBAnalyserServer))
            {
                request.DBAnalyserServerName = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == new Guid (request.DBAnalyserServer)).Name;
            }
            CubeGenerator.BuildCube(request,request.EmptyCube);
            //
            return Unit.Value;
        }


    }
}
