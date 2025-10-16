using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Services.Users;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsersAsync(string? username = null, string? email = null, string? role = null, int pageNumber = 1, int pageSize = 10);
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task DeleteUserAsync(int id);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}
