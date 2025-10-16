using MediatR;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Queries;

public class LoginQuery : IRequest<AuthResponseDto>
{
    public LoginDto LoginData { get; set; } = new();
}
