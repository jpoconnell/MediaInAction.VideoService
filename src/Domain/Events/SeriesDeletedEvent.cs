namespace VideoService2.Domain.Events;

public class SeriesDeletedEvent : BaseEvent
{
    public SeriesDeletedEvent(Series item)
    {
        Item = item;
    }

    public Series Item { get; }
}
