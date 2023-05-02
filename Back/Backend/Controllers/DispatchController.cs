
using Application.Database.Queries;
using Application.Dispatch.Commandes;
using Application.Servers.Qeuries.GetServers;
using Application.Tables.Queries.GetTables;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class DispatchController : ApiControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> Get(CreateDisptachCommand command)
        {
            var vm =  await Mediator.Send(command);
            return Ok(vm);
        }
    }
}
