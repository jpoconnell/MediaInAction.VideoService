using System;

namespace MediaInAction.VideoService.EpisodeAliasNs
{
    public class EpisodeAliasCreateDto
    {
        public Guid EpisodeId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}

