namespace VideoService2.Domain.Entities;

public class SeriesAlias : BaseAuditableEntity
{
    public Guid SeriesId { get; set; }
    public string IdType { get; set; }
    public string IdValue { get; set; }
    
    public SeriesAlias(string idType, string idValue)
    {
        IdType = idType;
        IdValue = idValue;
    }
    
}