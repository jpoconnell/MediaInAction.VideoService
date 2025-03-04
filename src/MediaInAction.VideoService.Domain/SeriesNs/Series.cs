using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.SeriesNs;

public class Series : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    
    public SeriesStatus SeriesStatus { get; set; }
    public string? ImageName { get; set; }
    public List<SeriesAlias> SeriesAliases { get; private set; }

    public Series()
    {
    }

    internal Series(
        [NotNull] string name,
        int firstAiredYear,
        SeriesStatus seriesStatus = SeriesStatus.New,
        string imageName = ""
    )
    {
        Id = Guid.NewGuid();
        Name = name;
        SeriesStatus = seriesStatus;
        FirstAiredYear = firstAiredYear;
        ImageName = imageName;
        SeriesAliases = new List<SeriesAlias>();
    }
    
    public void AddSeriesAlias(Guid seriesId, string idType, string idValue)
    {
        SeriesAliases.Add(new SeriesAlias(seriesId, idType, idValue));
    }

    public void SetSeriesAsInActive()
    {
        SeriesStatus = SeriesStatus.InActive;
    }
}