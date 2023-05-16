using Application.Calculation.Commandes;
using Application.Common.Interfaces;
using Application.Cube.Commandes.CreateCubeCommand;
using Application.Dispatch.Commandes;
using Application.Helper.OLAPCube;
using Domain.Entities;
using MediatR;
using Microsoft.AnalysisServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Calculation.Commandes
{
    public class CreateCalculationHandler : IRequestHandler<CreateCalculationCommand, CreateCalculationResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        private readonly IMediator _mediator;


        //public async Task<CreateCalculationHandler> Handle(CreateCalculationCommand request, CancellationToken cancellationToken)
        //{
        //    var serverSourceAnylser = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == new Guid(request.SourceServerAnalyseId));

        //    CubeGenerator.Calculation(request.SourceServerAnalyseId , request.SoureceAnalyserDb, request.Provider ,  request.Mes1, request.Mes2 , request.Opr,  request.Namecalculation) ;

        //    return null;
        //}


        Task<CreateCalculationResponse> IRequestHandler<CreateCalculationCommand, CreateCalculationResponse>.Handle(CreateCalculationCommand request, CancellationToken cancellationToken)
        {
            var serverSourceAnylser = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == new Guid(request.SourceServerAnalyseId));

            CubeGenerator.Calculation(request.SourceServerAnalyseId, request.SoureceAnalyserDb, request.Provider, request.Mes1, request.Mes2, request.Opr, request.Namecalculation);

            return null;
        }
    
    }
}