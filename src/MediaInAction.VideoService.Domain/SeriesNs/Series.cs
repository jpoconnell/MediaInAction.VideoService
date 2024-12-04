using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.SeriesAliasNs;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.SeriesNs;

public class Series : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public MediaType Type { get; set; }
    public MediaStatus MediaStatus { get; set; }
    public FileStatus EventStatus { get; set; }
    public bool IsActive { get; set; }
    public string ImageName { get; set; }
    public List<SeriesAlias> SeriesAliases { get; private set; }

    public Series()
    {
    }

    internal Series(
        [NotNull] string name,
        int firstAiredYear,
        [NotNull] MediaType seriesType,
        bool isActive = true,
        string imageName = ""
    )
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = seriesType;
        FirstAiredYear = firstAiredYear;
        IsActive = isActive;
        SeriesAliases = new List<SeriesAlias>();
    }
    
    public Series SetSeriesInactive()
    {
        IsActive = false;
        return this;
    }

    public void AddSeriesAlias(Guid seriesId, string idType, string idValue)
    {
        SeriesAliases.Add(new SeriesAlias(seriesId, idType, idValue));
        
     
    }
}