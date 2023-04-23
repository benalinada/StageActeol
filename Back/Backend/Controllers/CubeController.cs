using Application.Cube.Commandes.CreateCubeCommand;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class CubeController : ApiControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] CreateCubeCommand cmd)
        {
            var vm = await Mediator.Send(cmd);
            return Ok(vm);
        }
    }
}
