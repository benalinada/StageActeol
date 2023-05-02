using Application.Cube.Commandes.CreateCubeCommand;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dispatch.Commandes
{
    public class CreateDispatchHandler : IRequestHandler<CreateDisptachCommand, CreateDispatchResponse>
    {
        public async Task<CreateDispatchResponse> Handle(CreateDisptachCommand request, CancellationToken cancellationToken)
        {
            /*
             * request => information de cube 
            foreach (var item in request.TaregtServerId)
            {
                var cmd = new CreateCubeCommand()
                {
                    
                };
                var vm = await Mediator.Send(cmd);
            }*/
            Thread.Sleep(2000);
            return new CreateDispatchResponse()
            {

                Data = new List<DispatchLookUpDto> { new DispatchLookUpDto() { NameCube= request.NameCube,SourceServerName="testSource", TargetServerName="testtarget" ,SoureceDb=request.SoureceDb} }
            };
        }

       
    }
}
