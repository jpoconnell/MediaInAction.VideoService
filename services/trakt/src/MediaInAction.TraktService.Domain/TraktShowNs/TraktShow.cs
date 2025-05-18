using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShow : AuditedAggregateRoot<Guid>
{
    public string Slug { get; set; }
    
    [CanBeNull] public string ExternalId { get; set; }  // video series id
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }

    public bool Changed { get; set; }
    public TraktShowStatus Status { get; set; }
    
    public List<TraktShowAlias> TraktShowAliases { get; set; }
    private TraktShow()
    {
    }

    internal TraktShow(
        Guid id,
        [NotNull] string name, 
        [NotNull] string slug, 
        int year,
        TraktShowStatus status = TraktShowStatus.New
        ): base(id)
    {
        SetName(Check.NotNullOrWhiteSpace(name, nameof(name)));
        Slug = slug;
        SetYear(year);
        Status = status;
        TraktShowAliases = new List<TraktShowAlias>();
    }

    public TraktShow SetYear(int year)
    {
        if (year < 1940)
        {
            throw new ArgumentException($"{nameof(year)} can not be less than 1940!");
        }

        FirstAiredYear = year;
        return this;
    }
    
    public TraktShow SetName([NotNull] string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        Name = name;
        return this;
    }

    public TraktShow AddTraktShowAlias(Guid id, string idType, string idValue)
    {
        var existingAliasForShow = TraktShowAliases.SingleOrDefault(o => o.IdType == idType &&
            o.IdValue == idValue);

        if (existingAliasForShow != null)
        {

        }
        else
        {
            var traktAlias = new TraktShowAlias(id, idType, idValue);
            TraktShowAliases.Add(traktAlias);
        }
        return this;
    }
}