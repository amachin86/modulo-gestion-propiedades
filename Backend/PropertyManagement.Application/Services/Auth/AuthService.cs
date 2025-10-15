using MediatR;
using PropertyManagement.Application.Commands;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IMediator _mediator;

    public AuthService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var command = new LoginCommand
        {
            Username = loginDto.Username,
            Password = loginDto.Password
        };

        return await _mediator.Send(command);
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var command = new RegisterCommand
        {
            Username = registerDto.Username,
            Password = registerDto.Password,
            Email = registerDto.Email,
            Role = registerDto.Role
        };

        return await _mediator.Send(command);
    }
}
