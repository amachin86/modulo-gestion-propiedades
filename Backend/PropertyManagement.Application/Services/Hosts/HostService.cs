using AutoMapper;
using MediatR;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Services.Hosts;

public class HostService : IHostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HostService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HostDto>> GetHostsAsync(string? name = null, string? email = null, int pageNumber = 1, int pageSize = 10)
    {
        var hostRepository = _unitOfWork.Repository<Host>();
        var hosts = await hostRepository.GetAllAsync();

        // Apply filters
        if (!string.IsNullOrEmpty(name))
        {
            hosts = hosts.Where(h => h.FullName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(email))
        {
            hosts = hosts.Where(h => h.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
        }

        // Apply pagination
        hosts = hosts.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return _mapper.Map<IEnumerable<HostDto>>(hosts);
    }

    public async Task<HostDto> GetHostByIdAsync(int id)
    {
        var hostRepository = _unitOfWork.Repository<Host>();
        var host = await hostRepository.GetByIdAsync(id);

        if (host == null)
        {
            throw new KeyNotFoundException($"Host with ID {id} not found.");
        }

        return _mapper.Map<HostDto>(host);
    }

    public async Task<HostDto> CreateHostAsync(CreateHostDto createHostDto)
    {
        var host = _mapper.Map<Host>(createHostDto);

        var hostRepository = _unitOfWork.Repository<Host>();
        await hostRepository.AddAsync(host);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<HostDto>(host);
    }

    public async Task<HostDto> UpdateHostAsync(int id, UpdateHostDto updateHostDto)
    {
        var hostRepository = _unitOfWork.Repository<Host>();
        var host = await hostRepository.GetByIdAsync(id);

        if (host == null)
        {
            throw new KeyNotFoundException($"Host with ID {id} not found.");
        }

        _mapper.Map(updateHostDto, host);
        hostRepository.Update(host);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<HostDto>(host);
    }

    public async Task DeleteHostAsync(int id)
    {
        var hostRepository = _unitOfWork.Repository<Host>();
        var host = await hostRepository.GetByIdAsync(id);

        if (host == null)
        {
            throw new KeyNotFoundException($"Host with ID {id} not found.");
        }

        hostRepository.Remove(host);
        await _unitOfWork.SaveChangesAsync();
    }
}
