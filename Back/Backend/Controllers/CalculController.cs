using Application.Calculation.Queries.GetCalculation;
using Application.Cube.Commandes.CreateCubeCommand;
using Application.Messure.Queries.GetMessure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.Common;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class CalculController : ApiControllerBase
    {
        [HttpGet("{id}/{dbName}")]
        public async Task<IActionResult> get(string id,string dbName  )
        {
            var cmd = new GetCalculationQuery() { DBCubeName = dbName, ServerId = new Guid(id) };
            var vm = await Mediator.Send(cmd);
            return Ok(vm);
        }
    }
}
