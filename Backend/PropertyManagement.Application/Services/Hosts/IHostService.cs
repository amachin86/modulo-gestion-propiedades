using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Services.Hosts;

public interface IHostService
{
    Task<IEnumerable<HostDto>> GetHostsAsync(string? name = null, string? email = null, int pageNumber = 1, int pageSize = 10);
    Task<HostDto> GetHostByIdAsync(int id);
    Task<HostDto> CreateHostAsync(CreateHostDto createHostDto);
    Task<HostDto> UpdateHostAsync(int id, UpdateHostDto updateHostDto);
    Task DeleteHostAsync(int id);
}
