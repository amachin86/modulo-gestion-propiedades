using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PropertyManagement.Infrastructure.Repositories;

public class HostRepository : Repository<Host>, IHostRepository
{
    public HostRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Host?> GetByEmailAsync(string email)
    {
        return await _context.Hosts.FirstOrDefaultAsync(h => h.Email == email);
    }
}
