using AutoMapper;
using MediatR;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Queries;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync(string? username = null, string? email = null, string? role = null, int pageNumber = 1, int pageSize = 10)
    {
        var userRepository = _unitOfWork.Repository<User>();
        var users = await userRepository.GetAllAsync();

        // Apply filters
        if (!string.IsNullOrEmpty(username))
        {
            users = users.Where(u => u.Username.Contains(username, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(email))
        {
            users = users.Where(u => u.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(role))
        {
            users = users.Where(u => u.Role.Contains(role, StringComparison.OrdinalIgnoreCase));
        }

        // Apply pagination
        users = users.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var userRepository = _unitOfWork.Repository<User>();
        var user = await userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);
        user.CreatedAt = DateTime.UtcNow;
        user.LastLoginAt = null;

        // Hash password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

        var userRepository = _unitOfWork.Repository<User>();
        await userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var userRepository = _unitOfWork.Repository<User>();
        var user = await userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        _mapper.Map(updateUserDto, user);
        userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var userRepository = _unitOfWork.Repository<User>();
        var user = await userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        userRepository.Remove(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var query = new LoginQuery { LoginData = loginDto };
        return await _mediator.Send(query);
    }
}
