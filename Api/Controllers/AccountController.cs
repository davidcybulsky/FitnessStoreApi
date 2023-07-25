using Api.Dtos;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Endpoint odpowiada za logowanie użytkowników
        /// </summary>
        /// <param name="loginDto">
        /// Model transferu danych, który zawiera dane logowania - email i hasło
        /// </param>
        /// <returns>
        /// Zwraca token JWT, którym posługuje się użytkownik w celu autentykacji i autoryzacji
        /// </returns>

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
        {
            var token = await _service.LoginAsync(loginDto);
            return StatusCode(200, token);
        }

        /// <summary>
        /// Endpoint odpowiada za rejestracje użytkówników
        /// </summary>
        /// <param name="signUpDto">
        /// Model transferu danych zawierający dane do utworzenia konta
        /// </param>
        /// <returns>
        /// </returns>

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            await _service.SignUpAsync(signUpDto);
            return StatusCode(201);
        }

        /// <summary>
        /// Endpoint odpowiada za zwacanie danych konta użytkownika
        /// </summary>
        /// <returns>
        /// Model transferu danych dla konta
        /// </returns>
        /// <response code="200">Pomyślne pobranie konta</response>

        [Authorize]
        [HttpGet()]
        public async Task<ActionResult<AccountDto>> GetAccount()
        {
            var accountDto = await _service.GetAccountAsync();
            return StatusCode(200, accountDto);
        }

        /// <summary>
        /// Endpoint odpowiada za aktualizowanie konta
        /// </summary>
        /// <param name="updateAccountDto">
        /// Model transferu danych zawierający dane zaktualizowanego konta
        /// </param>
        /// <returns></returns>

        [Authorize]
        [HttpPut()]
        public async Task<ActionResult> UpdateAccount([FromBody] UpdateAccountDto updateAccountDto)
        {
            await _service.UpdateAccountAsync(updateAccountDto);
            return StatusCode(204);
        }

        /// <summary>
        /// Endpoint odpowiada za usuwanie konta użytownika
        /// </summary>
        /// <returns></returns>

        [Authorize]
        [HttpDelete()]
        public async Task<ActionResult> DeleteAccount()
        {
            await _service.DeleteAccountAsync();
            return StatusCode(204);
        }
    }
}
