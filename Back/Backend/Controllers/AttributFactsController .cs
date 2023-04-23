using Application.AttributFact.Queries.GetAttributFact;
using Application.Columns.Queries.GetColumns;
using Application.Database.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class AttributFactsController : ApiControllerBase
    {
        [HttpGet("{id}/{dbName}/{tableName}")]
        public async Task<IActionResult> Get(string id, string dbName,string tableName)
        {
            var vm = await Mediator.Send(new GetAttributFactQuery() { ServerId = new Guid(id) , DBName = dbName, TableName = tableName});
            return Ok(vm);
        }
    }
}
