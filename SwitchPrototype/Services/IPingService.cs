namespace SwitchPrototype.Services;

public interface IPingService
{
    public Task<bool> IsDevicePingable();
}