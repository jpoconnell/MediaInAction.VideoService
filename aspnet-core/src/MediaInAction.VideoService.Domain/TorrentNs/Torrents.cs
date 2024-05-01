using System;
using MediaInAction.VideoService.Enums;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace MediaInAction.VideoService.TorrentsNs
{
    public class Torrent :  AuditedAggregateRoot<Guid>
    {
        public string Comment { get; set; }
        public bool IsSeed { get; set; }
        public string Hash { get; set; }
        public bool Paused { get; set; }
        public double Ratio { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public long Added { get; set; }
        public double CompleteTime { get; set; }
        public string DownloadLocation { get; set; }
        
        public FileStatus TorrentStatus { get; set; }
        public MediaType Type { get; set; }
        public Guid MediaLink { get; set; }
        public Guid EpisodeLink { get; set; }
        public bool IsMapped { get; set; }
       
        protected Torrent()
        {
        }
        
        public Torrent(Guid id,
            string comment, 
            bool isSeed,
            string hash,
            bool paused, 
            double ratio,
            string message, 
            string name,
            string label,
            long added, 
            double completeTime, 
            string downloadLocation,
            FileStatus status,
            MediaType type,
            Guid mediaLink,
            Guid episodeLink ,
            bool isMapped
        )
            : base(id)
        {
            Comment = comment;
            IsSeed = isSeed;
            Hash = hash;
            Paused = paused;
            Ratio = ratio;
            Message = message;
            Name = name;
            Label = label;
            Added = added;
            CompleteTime = completeTime;
            DownloadLocation = downloadLocation;
            TorrentStatus = status;
            Type = type;
            MediaLink = mediaLink;
            EpisodeLink = episodeLink;
            IsMapped = isMapped;
        }
    }
}
