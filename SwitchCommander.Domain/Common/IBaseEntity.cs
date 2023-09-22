namespace SwitchCommander.Domain.Common;

public interface IBaseEntity 
{
    public long DateCreated { get; set; }
    public long? DateUpdated { get; set; }
    public long? DateDeleted { get; set; }
    
    public Guid Id { get; set; }
}