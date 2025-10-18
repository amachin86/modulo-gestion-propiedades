using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Queries;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PropertyManagement.Application.Handlers;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public LoginQueryHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var userRepository = _unitOfWork.Repository<User>();
        var users = await userRepository.FindAsync(u => u.Username == request.LoginData.Username);

        var user = users.FirstOrDefault();

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginData.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        // Update last login
        user.LastLoginAt = DateTime.UtcNow;
        userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        // Generate JWT token
        var token = GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
           /* User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            }*/
        };
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "default-secret-key"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("userId", user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
