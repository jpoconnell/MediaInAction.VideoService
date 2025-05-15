namespace VideoService2.Domain.Events;

public class SeriesCompletedEvent : BaseEvent
{
    public SeriesCompletedEvent(Series item)
    {
        Item = item;
    }

    public Series Item { get; }
}
