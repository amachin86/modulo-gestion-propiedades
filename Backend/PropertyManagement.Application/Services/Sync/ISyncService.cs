namespace PropertyManagement.Application.Services.Sync;

public interface ISyncService
{
    Task SyncPropertyAsync(Guid propertyId, string action);
}
