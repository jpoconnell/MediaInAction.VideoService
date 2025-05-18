using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.TraktService.TraktRequests
{
    public class TraktRequest : CreationAuditedAggregateRoot<Guid>, ISoftDelete
    {
        [NotNull] public string Command { get; protected set; }
        [NotNull] public string OrderId { get; protected set; }
        public int OrderNo { get; protected set; }
        [CanBeNull] public string BuyerId { get; protected set; }
        public TraktRequestState State { get; protected set; }
        [CanBeNull] public string FailReason { get; protected set; }
        public bool IsDeleted { get; set; }
        public ICollection<TraktRequestSeries> Seriess { get; } = new List<TraktRequestSeries>();
        private TraktRequest()
        {
        }

        public TraktRequest(Guid id,
            [NotNull] string orderId,
            int orderNo,
            [NotNull] string currency,
            [CanBeNull] string buyerId = null) : base(id)
        {
            OrderId = Check.NotNullOrWhiteSpace(orderId, nameof(orderId), minLength: TraktRequestConsts.MinOrderIdLength, maxLength: TraktRequestConsts.MaxOrderIdLength);
            Command = Check.NotNullOrWhiteSpace(currency, nameof(currency), maxLength: TraktRequestConsts.MaxCurrencyLength);
            BuyerId = buyerId;
            OrderNo = orderNo;
        }

        public virtual void SetAsCompleted()
        {
            if (State == TraktRequestState.Completed)
            {
                return;
            }

            State = TraktRequestState.Completed;
            FailReason = null;
        }

        public virtual void SetAsFailed(string failReason)
        {
            if (State != TraktRequestState.Failed)
            {
                return;
            }

            State = TraktRequestState.Failed;
            FailReason = failReason;
            
        }
    }
}