using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Interfaces;

public interface IHostRepository : IRepository<Host>
{
    Task<Host?> GetByEmailAsync(string email);
}
