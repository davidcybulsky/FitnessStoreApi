using Api.Dtos;

namespace Api.Interfaces
{
    public interface IAccountService
    {
        Task<TokenDto> LoginAsync(LoginDto loginDto);
        Task SignUpAsync(SignUpDto signUpDto);
    }
}
