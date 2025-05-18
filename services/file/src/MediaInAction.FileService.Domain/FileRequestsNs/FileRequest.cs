using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.FileService.FileRequestsNs
{
    public class FileRequest : AuditedAggregateRoot<Guid>
    {
       
        public FileOperation Operation { get; set; }
        public FileRequestState State { get; set; }
        
        [CanBeNull] public string FailReason { get; protected set; }

        public List<FileEntry> Files { get; set; }
      

        public FileRequest()
        {
        }
        
        
        public virtual void SetAsCompleted()
        {
            if (State == FileRequestState.Completed)
            {
                return;
            }

            State = FileRequestState.Completed;
            FailReason = null;
            
        }

        public virtual void SetAsFailed(string failReason)
        {
            if (State != FileRequestState.Failed)
            {
                return;
            }

            State = FileRequestState.Failed;
            FailReason = failReason;
        }
   }
}
