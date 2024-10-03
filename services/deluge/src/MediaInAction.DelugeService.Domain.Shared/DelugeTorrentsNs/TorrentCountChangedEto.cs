using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.DelugeService.DelugeTorrentsNs
{
    [Serializable]
    public class TorrentCountChangedEto : EtoBase
    {
        public Guid Id { get; }

        public int OldCount { get; set; }

        public int CurrentCount { get; set; }

        private TorrentCountChangedEto()
        {
            //Default constructor is needed for deserialization.
        }

        public TorrentCountChangedEto(Guid id, int oldCount, int currentCount)
        {
            Id = id;
            OldCount = oldCount;
            CurrentCount = currentCount;
        }
    }
}