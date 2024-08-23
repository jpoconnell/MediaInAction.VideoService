using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.trakt.TraktShowNs
{
    public class TraktShowAliasCreatedEto : EtoBase
    {

        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}