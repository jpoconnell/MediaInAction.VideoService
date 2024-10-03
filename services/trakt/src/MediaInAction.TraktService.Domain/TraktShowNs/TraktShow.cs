using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.TraktShowAliasNs;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShow : AuditedAggregateRoot<Guid>
{
    public string Slug { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<TraktShowAlias> TraktShowAliases { get; set; }
    public bool Changed { get; set; }
    public bool Accepted { get; set; }
    public FileStatus TraktStatus { get; set; }
    private TraktShow()
    {
    }

    internal TraktShow(
        Guid id,
        [NotNull] string name, 
        int year,
        List<TraktShowAlias> traktShowAliases,
        string slug = "" 
        )
    {
        Id = id;
        SetName(Check.NotNullOrWhiteSpace(name, nameof(name)));
        Slug = slug;
        SetYear(year);
        Changed = true;
        TraktShowAliases = traktShowAliases;
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

    public void SetAccepted()
    {
        Accepted = true;
    }
}