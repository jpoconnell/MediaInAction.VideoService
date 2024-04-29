using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediaInAction.VideoService.EpisodeAliasNs;

namespace  MediaInAction.VideoService.EpisodeNs.Dtos
{
    public class EpisodeCreateDto
    {
        [Required]
        public Guid SeriesId { get; set; }
        [Required]
        public int SeasonNum { get; set; }
        [Required]
        public int EpisodeNum { get; set; }
        public DateTime AiredDate { get; set; }
        
        public string EpisodeStatus { get; set; }
        public int EpisodeStatusId { get; set; }
        public string EpisodeName { get; set; }
        public string AltEpisodeId { get; set; }
        public string SeasonEpisode { get; set; }
        public List<EpisodeAliasCreateDto> EpisodeAliases { get; set; } 
    }
}

