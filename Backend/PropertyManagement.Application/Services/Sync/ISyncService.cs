namespace PropertyManagement.Application.Services.Sync;

public interface ISyncService
{
    Task SyncPropertyAsync(int propertyId, string action);
}
