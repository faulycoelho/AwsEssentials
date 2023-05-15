using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Contracts.Requests;
using Users.Api.Contracts.Responses;
using Users.Api.Services;

namespace Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("users")]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            var user = _mapper.Map<Domain.User>(request);
            await _userService.CreateAsync(user);

            var userResponse = _mapper.Map<UserResponse>(user);

            return CreatedAtAction("Get", new { userResponse.Id }, userResponse);
        }

        [HttpGet("users/{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            var userResponse = _mapper.Map<UserResponse>(user);

            return Ok(userResponse);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var usersResponse = _mapper.Map<IEnumerable<UserResponse>>(users);

            return Ok(usersResponse);
        }

        [HttpPut("users/{id:int}")]
        public async Task<IActionResult> Update(
            [FromMultiSource] UpdateUserRequest request)
        {
            var existingUser = await _userService.GetAsync(request.Id);

            if (existingUser is null)
            {
                return NotFound();
            }

            var user = _mapper.Map<Domain.User>(request.User);
            await _userService.UpdateAsync(user);

            var userResponse = _mapper.Map<UserResponse>(user);

            return Ok(userResponse);
        }

        [HttpDelete("users/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
