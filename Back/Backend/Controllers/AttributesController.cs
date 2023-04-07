using Application.Attributes.Queries.GetAttribute;
using Application.Columns.Queries.GetColumns;
using Application.Database.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace Backend.Controllers
{
    public class AttributesController : ApiControllerBase
    {
        [HttpGet("{id}/{dbName}/{tableName}")]
        public async Task<IActionResult> Get(string id, string dbName, string tableName, [FromQuery] string[] dimenstions)
        {
            var vm = await Mediator.Send(new GetAttributeQuery() { ServerId = new Guid(id) , DBName = dbName,TableName=tableName,Dimentions=dimenstions } );
            return Ok(vm);
        }
    }
}
