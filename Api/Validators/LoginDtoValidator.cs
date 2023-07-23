using Api.Dtos;
using Api.Entities;
using FluentValidation;

namespace Api.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator(ApiContext db)
        {
            RuleFor(loginDto => loginDto.Email)
                .EmailAddress()
                .WithMessage("This is not an email address");
        }
    }
}
