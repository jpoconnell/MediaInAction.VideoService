using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace MediaInAction.TraktService.TraktEpisodeNs.Dtos
{
    public class TraktEpisodeDto 
    {
        public Guid Id { get; set; }
        public string SeriesId { get; set; }
        [CanBeNull] public string ExternalId { get; set; }
        public string Slug { get;  set; }
        public int SeasonNum { get;  set; }
        public int EpisodeNum { get;  set; }
        public string EpisodeName { get; set; }
        public string AltEpisodeId { get; set; }
        public DateTime AiredDate { get; set; }
        public List<TraktEpisodeAliasDto> TraktEpisodeAliasDtos { get; set; }
        
    }
}