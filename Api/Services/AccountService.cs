﻿using Api.Dtos;
using Api.Entities;
using Api.Exceptions;
using Api.Interfaces;
using Api.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApiContext _db;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(ApiContext db,
                              IMapper mapper,
                              JwtSettings jwtSettings,
                              IPasswordHasher<User> passwordHasher)
        {
            _db = db;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
            _passwordHasher = passwordHasher;
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email) ?? throw new BadRequestException("Bad email or password.");

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

            var jwt = new TokenDto { Token = "bearer " + tokenHandler.WriteToken(token) };

            return jwt;
        }

        public async Task<int> SignUpAsync(SignUpDto signUpDto)
        {

            var newUser = _mapper.Map<User>(signUpDto);
            await _db.AddAsync(newUser);

            var hashedPassword = _passwordHasher.HashPassword(newUser, signUpDto.Password);
            newUser.HashedPassword = hashedPassword;

            await _db.SaveChangesAsync();

            return newUser.Id;
        }
    }
}
