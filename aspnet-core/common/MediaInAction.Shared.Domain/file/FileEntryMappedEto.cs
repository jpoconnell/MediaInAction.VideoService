using System;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.Shared.Domain.file;

[EventName("MediaInAction.FileEntry.Cancelled")]
public class FileEntryMappedEto : EtoBase
{
    public Guid PaymentRequestId { get; set; }
    public Guid FileEntryId { get; set; }
    public int FileEntryNo { get; set; }
    public DateTime FileEntryDate { get; set; }

}