using Application.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var vm =  await Mediator.Send(new GetUserQuery() { Email = email });
            return Ok(vm);
        }
    }
}
