using Microsoft.Extensions.Logging;

namespace VideoService2.Application.SeriesAliases.EventHandlers;

public class SeriesAliasCreatedEventHandler : INotificationHandler<SeriesAliasCreatedEvent>
{
    private readonly ILogger<SeriesAliasCreatedEventHandler> _logger;

    public SeriesAliasCreatedEventHandler(ILogger<SeriesAliasCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SeriesAliasCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("VideoService2 Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
