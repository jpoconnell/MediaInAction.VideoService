using Microsoft.Extensions.Logging;

namespace VideoService2.Application.SeriesAliases.EventHandlers;

public class SeriesAliasCompletedEventHandler : INotificationHandler<SeriesAliasCompletedEvent>
{
    private readonly ILogger<SeriesAliasCompletedEventHandler> _logger;

    public SeriesAliasCompletedEventHandler(ILogger<SeriesAliasCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SeriesAliasCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("VideoService2 Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
