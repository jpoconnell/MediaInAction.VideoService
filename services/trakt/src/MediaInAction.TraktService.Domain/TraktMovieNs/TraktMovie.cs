using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.TraktService.TraktMovieNs;

public class TraktMovie : AuditedAggregateRoot<Guid>
{
    [CanBeNull] public string ExternalId { get; set; }  // video movie id
    public string Slug { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<TraktMovieAlias> TraktMovieAliases { get; set; }
    public TraktMovieStatus Status { get; set; }
    private TraktMovie()
    {
    }

    internal TraktMovie(
        Guid id,
        [NotNull] string name, 
        int year,
        string slug,
        TraktMovieStatus status = TraktMovieStatus.New
        )
        : base(id)
    {
        SetName(Check.NotNullOrWhiteSpace(name, nameof(name)));
        SetYear(year);
        Status = status;
        Slug = slug;
        TraktMovieAliases = new List<TraktMovieAlias>();
    }

    public TraktMovie SetYear(int year)
    {
        if (year < 1940)
        {
            throw new ArgumentException($"{nameof(year)} can not be less than 1940!");
        }

        FirstAiredYear = year;
        return this;
    }
    
    internal TraktMovie ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    
    public TraktMovie SetName([NotNull] string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        Name = name;
        return this;
    }

    public TraktMovie AddTraktMovieAlias(Guid id, string idType, string idValue)
    {
        var existingAliasForMovie = TraktMovieAliases.SingleOrDefault(o => o.IdType == idType && o.IdValue == idValue);

        if (existingAliasForMovie != null)
        {
        }
        else
        {
            var traktAlias = new TraktMovieAlias(id, idType, idValue);
            TraktMovieAliases.Add(traktAlias);
           
        }
        return this;
    }
}