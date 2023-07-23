using Api.Dtos;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
        {
            var token = await _service.LoginAsync(loginDto);
            return StatusCode(200, token);
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            await _service.SignUpAsync(signUpDto);
            return StatusCode(201);
        }
    }
}
