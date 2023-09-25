using Microsoft.Extensions.Logging;

namespace SwitchCommander.Persistence.Services.BackgroundTasks;

public class LicenseKeyBackgroundService : IntervalHostedService<LicenseKeyService>
{
    private static LicenseKeyService _licenseKeyService;

    public LicenseKeyBackgroundService(
        ILogger<IntervalHostedService<LicenseKeyService>> logger, LicenseKeyService licenseKeyService) :
        base(logger, 5000, Action)
    {
        _licenseKeyService = licenseKeyService;
    }

    private static void Action()
    {
        if (_licenseKeyService.IsValid()) Console.WriteLine("License key is  update to date");
        Console.WriteLine("License key is not  update to date");
    }
}