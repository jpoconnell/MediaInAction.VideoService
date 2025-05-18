using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.FileService.FileMappingNs
{
    public class FileMapping : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public string SearchString { get; set; }
        public List<Guid> FileEntryIds { get; set; }
        public bool IsSent { get; set; }

        public FileMapping()
        {
        }
   }
}
