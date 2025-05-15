using System.ComponentModel.DataAnnotations;

namespace VideoService2.Domain.Entities;

public class EpisodeAlias: BaseAuditableEntity
{
    [Required]
    public Guid EpisodeId {get; set; }
    [Required]
    public string IdType { get; set; }
    [Required]
    public string IdValue { get; set; }
    
    public EpisodeAlias(string idType, string idValue)
    {
        IdType = idType;
        IdValue = idValue;
    }
    
}

