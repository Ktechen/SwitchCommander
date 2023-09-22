namespace SwitchCommander.Persistence.Services;

public class LicenseKeyService : ILicenseKeyService
{
    private bool status;
    
    public bool IsValid()
    {
        return status = !status;
    }
}