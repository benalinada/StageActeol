
using Application.Database.Queries;
using Application.Servers.Qeuries.GetServers;
using Application.Tables.Queries.GetTables;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class TablesController : ApiControllerBase
    {
        [HttpGet("{id}/{dBName}")]
        public async Task<IActionResult> Get(string id,string dBName)
        {
            var vm =  await Mediator.Send(new GetTableQuery() { ServerId = new Guid(id),DBName=dBName });
            return Ok(vm);
        }
    }
}
