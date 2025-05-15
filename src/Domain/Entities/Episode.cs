using System.ComponentModel.DataAnnotations;

namespace VideoService2.Domain.Entities;

public class Episode : BaseAuditableEntity
{
    [Required]
    public Guid SeriesId { get; set; }
    [Required]
    public int SeasonNum { get; set; }
    [Required]
    public int EpisodeNum { get; set; }
    public MediaStatus Status { get; set; }

    public DateTime AiredDate { get; set; }
    public string EpisodeName { get; set; }
    public string AltEpisodeId { get; set; }
    public string SeasonEpisode { get; set; }
    public string Source { get; set; }
    public List<EpisodeAlias> EpisodeAliases { get;  set; }
    
    private Episode(string episodeName, string altEpisodeId, string seasonEpisode, string source, List<EpisodeAlias> episodeAliases)
    {
        EpisodeName = episodeName;
        AltEpisodeId = altEpisodeId;
        SeasonEpisode = seasonEpisode;
        Source = source;
        EpisodeAliases = episodeAliases;
    }
    
}
