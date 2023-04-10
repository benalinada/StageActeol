using Application.Cube.Commandes.CreateCubeCommand;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubeController : ApiControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> Post()
        {
            var cmd = new CreateCubeCommand()
            {
                CubeDBName = "test",
                DBName = "DW_Bookings",
                DBServerName = "localhost",
                FactTableName = "Fact_Bookings",
                DimensionTableCount = 1,

                TableNamesAndKeys = new[,] {
                    { "Dim_Walkin", "ID", "Fact_Bookings", "Walkin" }

                }
            };
        
        
            var vm = await Mediator.Send(cmd);
            return Ok(vm);
        }
    }
}
