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
    internal class CreateCubeHandler : IRequestHandler<CreateCubeCommand, Unit>
    {
        public async Task<Unit> Handle(CreateCubeCommand request, CancellationToken cancellationToken)
        {
            CubeGenerator.BuildCube(request);
            //
            return Unit.Value;
        }


    }
}
