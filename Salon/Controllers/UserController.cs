using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MediatR;
using Salon.Queries.User;
using Salon.Responses.User;
using Salon.Commands.User;

namespace Salon.Controllers
{
    //[Authorize]
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _iMediator;
        public UserController(IMediator iMediator)
        {
            _iMediator = iMediator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> SearchAsync([FromQuery] GetUsersByParamsQuery query)
        {
            return Ok(await _iMediator.Send(query));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetByIdAsync(long id)
        {
            return Ok(await _iMediator.Send(new GetUserByIdQuery(id)));
        }
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] CreateUserCommand command)
        {
            return Ok(await _iMediator.Send(command));
        }
        [HttpPut]
        public async Task<ActionResult<UserResponse>> UpdateAsync([FromBody] UpdateUserCommand command)
        {
            return Ok(await _iMediator.Send(command));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> ActiveByIdAsync(long id)
        {

            return Ok(await _iMediator.Send(new PartialUpdateUserCommand(id)));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(long id)
        {
            return Ok(await _iMediator.Send(new DeleteUserCommand(id)));
        }
    }
}