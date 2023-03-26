
using Application.Servers.Qeuries.GetServers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class ServersController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var vm =  await Mediator.Send(new GetServersQuery() { UserId = new Guid(id) });
            return Ok(vm);
        }
    }
}
