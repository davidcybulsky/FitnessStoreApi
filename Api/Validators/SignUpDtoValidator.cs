using Api.Dtos;
using Api.Entities;
using FluentValidation;

namespace Api.Validators
{
    public class SignUpDtoValidator : AbstractValidator<SignUpDto>
    {
        public SignUpDtoValidator(ApiContext db)
        {
            RuleFor(signUpDto => signUpDto.Email)
                .EmailAddress()
                .Must((signUpDto, context) =>
                {
                    var isEmailInDb = db.Users.Any(u => u.Email == signUpDto.Email);
                    return !isEmailInDb;
                }).WithMessage("This email has already been taken.");

            RuleFor(signUpDto => signUpDto.Password)
                .Equal(signUpDto => signUpDto.Password)
                .WithMessage("Passwords are not equal");

            RuleFor(signUpDto => signUpDto.Password)
                .MinimumLength(8)
                .WithMessage("This password is too short");

            RuleFor(signUpDto => signUpDto.Password)
                .MaximumLength(32)
                .WithMessage("This password is too long");
        }
    }
}
