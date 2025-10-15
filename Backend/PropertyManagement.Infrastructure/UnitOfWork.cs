using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Data;
using PropertyManagement.Infrastructure.Repositories;

namespace PropertyManagement.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IRepository<Property>? _propertyRepository;
    private IRepository<Host>? _hostRepository;
    private IRepository<User>? _userRepository;
    private IRepository<DomainEvent>? _domainEventRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<T> Repository<T>() where T : class
    {
        if (typeof(T) == typeof(Property))
        {
            if (_propertyRepository == null)
            {
                _propertyRepository = new Repository<Property>(_context);
            }
            return (IRepository<T>)_propertyRepository;
        }
        else if (typeof(T) == typeof(Host))
        {
            if (_hostRepository == null)
            {
                _hostRepository = new Repository<Host>(_context);
            }
            return (IRepository<T>)_hostRepository;
        }
        else if (typeof(T) == typeof(User))
        {
            if (_userRepository == null)
            {
                _userRepository = new Repository<User>(_context);
            }
            return (IRepository<T>)_userRepository;
        }
        else if (typeof(T) == typeof(DomainEvent))
        {
            if (_domainEventRepository == null)
            {
                _domainEventRepository = new Repository<DomainEvent>(_context);
            }
            return (IRepository<T>)_domainEventRepository;
        }

        throw new ArgumentException($"Repository for type {typeof(T).Name} is not supported.");
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
