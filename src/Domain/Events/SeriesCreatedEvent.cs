namespace VideoService2.Domain.Events;

public class SeriesCreatedEvent : BaseEvent
{
    public SeriesCreatedEvent(Series item)
    {
        Item = item;
    }

    public Series Item { get; }
}
