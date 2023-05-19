using Application.Common.Interfaces;
using Application.Cube.Commandes.CreateCubeCommand;
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
using System.Security.Permissions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dispatch.Commandes
{
    public class CreateDispatchHandler : IRequestHandler<CreateDisptachCommand, CreateDispatchResponse>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        private readonly IMediator _mediator;

        public CreateDispatchHandler(IApplicationDBContext applicationDBContext, IMediator mediator)
        {
            _applicationDBContext = applicationDBContext;
            _mediator = mediator;
        }
        public async Task<CreateDispatchResponse> Handle(CreateDisptachCommand request, CancellationToken cancellationToken)
        {

            var serverSourceAnylser = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == new Guid(request.SourceServerAnalyseId));

            foreach (var item in request.TargetServerAnalyseId)
            {
                var serverTargetAnalyser = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == new Guid(item));
                var serverTargetEngine = _applicationDBContext.Servers.SingleOrDefault(s => s.Id == new Guid(request.TargetServerEngineId));
                var existCube = CubeGenerator.GetCube(serverTargetAnalyser.Name, request.SoureceAnalyserDb, request.CubeName, request.Provider);

                if (existCube == null)
                {
                    throw new Exception("Cube not found");
                }

                // creete empty cube in target
                var mesures = existCube.MeasureGroups[0].Measures;
               
                for (int i = 0; i < mesures.Count; i++)
                {

                }
                var command = new CreateCubeCommand()
                {
                    CubeDBName = request.NewDbName,
                    DBAnalyserServer = item,
                    CubeDataSourceName = existCube.DataSource.Name,
                    CubeDataSourceViewName = existCube.DataSourceView.Name,
                    DBAnalyserServerName = serverTargetAnalyser.Name,
                    DBEngineServer = request.TargetServerEngineId,
                    DBName = request.TargetEngineDb,
                    DimensionTableCount = existCube.Dimensions.Count,
                    ProviderName = request.Provider,
                    FactTableName = existCube.MeasureGroups[0].Name,
                    MessureCout = existCube.MeasureGroups[0].Measures.Count,
                };
                var dimentions = existCube.MeasureGroups[0].Dimensions;
                command.Messurecalcl = new string[existCube.MeasureGroups[0].Measures.Count, 3];
                for (int i = 0;i < command.MessureCout; i++)
                {
                    command.Messurecalcl[i, 0] = existCube.MeasureGroups[0].Measures[i].ID.ToString();
                    command.Messurecalcl[i, 1] = existCube.MeasureGroups[0].Measures[i].AggregateFunction.ToString();
                    command.Messurecalcl[i, 2] = existCube.MeasureGroups[0].Measures[i].ToString();
                }
                
                command.TableNamesAndKeys = new string[dimentions.Count, 4];
                for (int i = 0; i < dimentions.Count; i++)
                {
                    var regMesGroup = (RegularMeasureGroupDimension)dimentions[i];
                    for (int j = 0; j < regMesGroup.Attributes.Count; j++)
                    {
                        var attribue = regMesGroup.Attributes[j];
                        for (int k = 0; k < attribue.KeyColumns.Count; k++)
                        {
                            var columnBinding = (ColumnBinding)attribue.KeyColumns[k].Source;
                            command.TableNamesAndKeys[i, 0] = regMesGroup.ToString();
                            command.TableNamesAndKeys[i, 1] = attribue.ToString();
                            command.TableNamesAndKeys[i,2] = columnBinding.TableID; // fact table
                            command.TableNamesAndKeys[i,3] = columnBinding.ColumnID; // id de dimention dans fact table
                            

                        }
                    }
                }

                var x = JsonConvert.SerializeObject(command);
                var vm = await _mediator.Send(command);
                //CubeGenerator.Clone(serverSourceAnylser.Name, request.SoureceAnalyserDb, serverTargetAnalyser.Name, request.NameCube, "SampleCube", command.ProviderName);
            }
            return new CreateDispatchResponse()
            {

                Data = new List<DispatchLookUpDto> { new DispatchLookUpDto() { NameCube = request.CubeName, SourceServerName = "testSource", TargetServerName = "testtarget" } }
            };
        }


    }
}
