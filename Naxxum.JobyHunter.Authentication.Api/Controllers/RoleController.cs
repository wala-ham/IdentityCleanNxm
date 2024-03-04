using MediatR;
using Microsoft.AspNetCore.Mvc;
using Naxxum.JobyHunter.Authentication.Application.Commands.Role.Create;
using Naxxum.JobyHunter.Authentication.Application.Commands.Role.Delete;
using Naxxum.JobyHunter.Authentication.Application.Commands.Role.Update;
using Naxxum.JobyHunter.Authentication.Application.DTOs;
using Naxxum.JobyHunter.Authentication.Application.Queries.Role;

namespace Naxxum.JobyHunter.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        public readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<ActionResult> CreateRoleAsync(RoleCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<RoleResponseDTO>))]
        public async Task<IActionResult> GetRoleAsync()
        {
            return Ok(await _mediator.Send(new GetRoleQuery()));
        }


        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(RoleResponseDTO))]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
        {
            return Ok(await _mediator.Send(new GetRoleByIdQuery() { RoleId = id }));
        }

        [HttpDelete("Delete/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            return Ok(await _mediator.Send(new DeleteRoleCommand()
            {
                RoleId = id
            }));
        }

        [HttpPut("Edit/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> EditRole(string id, [FromBody] UpdateRoleCommand command)
        {
            if (id == command.Id)
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
