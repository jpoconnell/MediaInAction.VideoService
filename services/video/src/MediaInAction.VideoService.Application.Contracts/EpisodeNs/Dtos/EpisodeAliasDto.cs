using System;

namespace MediaInAction.VideoService.EpisodeNs.Dtos
{
    public class EpisodeAliasDto
    {
        public Guid EpisodeId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}

