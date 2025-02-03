﻿using System;
using JetBrains.Annotations;
using MediaInAction.VideoService.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.ToBeMappedNs
{
    public class ToBeMapped :  AuditedAggregateRoot<Guid>
    {
        public string Alias { get; set; }
        public FromService? FromService { get; set; }
        [CanBeNull] public string FromId { get; set; }
        public int Tries  { get; set; }
        public MediaType Type { get; set; }
        public bool Processed { get; set; }
        protected ToBeMapped() { }
        
        public ToBeMapped(Guid id, string alias, int tries = 0, bool processed = false )
            : base(id)
        {
            Alias = alias;
            Processed = processed;
            Tries = tries;
        }
    }
}
