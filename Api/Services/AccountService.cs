using Api.Dtos;
using Api.Entities;
using Api.Exceptions;
using Api.Interfaces;
using Api.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApiContext _db;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(ApiContext db, IOptions<JwtSettings> jwtSettings, IPasswordHasher<User> passwordHasher)
        {
            _db = db;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();

            var user = _db.Users.FirstOrDefault(u => u.Email == loginDto.Email);

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, loginDto.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Bad email or password.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                Expires = DateTime.UtcNow.AddDays(int.Parse(_jwtSettings.ExpiresInDays)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = new TokenDto { Token = tokenHandler.WriteToken(token) };

            return jwt;
        }

        public Task SignUpAsync(SignUpDto signUpDto)
        {
            throw new NotImplementedException();
        }
    }
}
