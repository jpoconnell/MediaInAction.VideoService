using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.EmbyService.EmbyRequestsNs
{
    public class EmbyRequest : AuditedAggregateRoot<Guid>
    {
      //  public EmbyOperation Operation { get; set; }
        public EmbyRequestState State { get; set; }
        
        [CanBeNull] public string FailReason { get; protected set; }

       // public List<EmbyRequestItem> Embys { get; set; }
      

        private EmbyRequest()
        {
        }
        
        
        public virtual void SetAsCompleted()
        {
            if (State == EmbyRequestState.Completed)
            {
                return;
            }

            State = EmbyRequestState.Completed;
            FailReason = null;
        }

        public virtual void SetAsFailed(string failReason)
        {
            if (State != EmbyRequestState.Failed)
            {
                return;
            }

            State = EmbyRequestState.Failed;
            FailReason = failReason;
        }
   }
}
