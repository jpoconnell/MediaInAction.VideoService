using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.MovieAliasNs;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.MovieNs;

public class Movie : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public MovieStatus MovieStatus { get; set; }
    public string ImageName { get; set; }
    public List<MovieAlias> MovieAliases { get; private set; }

    public Movie()
    {
    }

    internal Movie(
        Guid id,
        [NotNull] string name,
        int firstAiredYear,
        [NotNull] MediaType movieType,
        bool isActive = true,
        string imageName = ""
    )
    {
        Id = id;
        Name = name;
        FirstAiredYear = firstAiredYear;
        MovieStatus = MovieStatus.New;
        MovieAliases = new List<MovieAlias>();
    }
    
    public Movie AddMovieAlias(Guid id, Guid movieId, string idType, string idValue )
    {
        var existingAliasForMovie = MovieAliases.SingleOrDefault(o => o.MovieId == movieId &&
            o.IdType == idType && 
            o.IdValue == idValue);

        if (existingAliasForMovie != null)
        {

        }
        else
        {
            var movieAlias = new MovieAlias(id, movieId, idType, idValue);
            MovieAliases.Add(movieAlias);
        }

        return this;
    }
    
    public Movie SetMovieInactive()
    {
        MovieStatus = MovieStatus.InActive;
        return this;
    }
}