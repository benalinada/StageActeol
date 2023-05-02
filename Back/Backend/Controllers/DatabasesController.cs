
using Application.Database.Queries;
using Application.Database.Queries.GetDataBaseSql;
using Application.Servers.Qeuries.GetServers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class DataBasesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var vm =  await Mediator.Send(new Application.Database.Queries.GetDataBaseQuery() { ServerId = new Guid(id) });
            return Ok(vm);
        }
    }
}
