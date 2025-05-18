using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisode : AuditedAggregateRoot<Guid>
{
    public string SeriesId { get; set; }
    public string SeriesSlug { get; set; }
    [CanBeNull] public string ExternalId { get; set; }  // trakt episode id
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    [CanBeNull] public string EpisodeName { get; set; }
    public DateTime AiredDate { get; set; }
    public List<TraktEpisodeAlias> TraktEpisodeAliases { get; set; }
    public TraktEpisodeStatus TraktStatus { get; set; }
    private TraktEpisode()
    {
        
    }
    
    internal TraktEpisode(
        Guid id,
        string seriesId, 
        string seriesSlug,
        int seasonNum,
        int episodeNum, 
        DateTime? airedDate,
        string name = "", 
        TraktEpisodeStatus status = TraktEpisodeStatus.New)
    {
        Id = id;
        SeriesId = seriesId;
        SeasonNum = seasonNum;
        EpisodeNum = episodeNum;
        EpisodeName = name;

        if (airedDate != null)
        {
            AiredDate = (DateTime) airedDate;
        }
        else
        {
            AiredDate = DateTime.MinValue;
        }
        
        TraktStatus = status;
        TraktEpisodeAliases = new List<TraktEpisodeAlias>();
    }
    
    public TraktEpisode SetName([NotNull] string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        EpisodeName = name;
        return this;
    }

    public TraktEpisode AddTraktEpisodeAlias(Guid id, string idType, string idValue)
    {
        var existingAliasForEpisode = TraktEpisodeAliases.SingleOrDefault(o => o.IdType == idType && o.IdValue == idValue);

        if (existingAliasForEpisode != null)
        {
        }
        else
        {
            var traktAlias = new TraktEpisodeAlias(id, idType, idValue);
            TraktEpisodeAliases.Add(traktAlias);
           
        }
        return this;
    }
}