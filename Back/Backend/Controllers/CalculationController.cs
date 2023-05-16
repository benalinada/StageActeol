
using Application.Calculation.Commandes;
using Application.Database.Queries;
using Application.Dispatch.Commandes;
using Application.Servers.Qeuries.GetServers;
using Application.Tables.Queries.GetTables;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CalculationController : ApiControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> Get(CreateCalculationCommand command)
        {
            var vm =  await Mediator.Send(command);
            return Ok(vm);
        }
    }
}
