using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.EmbyService;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.SeriesNs;

public class EmbyServiceShowCreatedEventHandler : IDistributedEventHandler<EmbyShowCreatedEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<EmbyServiceShowCreatedEventHandler> _logger;
    private readonly SeriesManager _seriesManager;
    
    public EmbyServiceShowCreatedEventHandler(
        IDistributedEventBus eventBus,
        SeriesManager seriesManager,
        ILogger<EmbyServiceShowCreatedEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        _seriesManager = seriesManager;
    }

    public async Task HandleEventAsync(EmbyShowCreatedEto eventData)
    {
        var tmpName = eventData.ShowName.ToLower();

        var acceptedFile = await _seriesManager.AcceptEmbyShowAsync(eventData);

        _logger.LogInformation("Sending Emby Show Accepted Event");
        await PublishEmbyShowAcceptedEvent(eventData);
    }

    private async Task PublishEmbyShowAcceptedEvent(EmbyShowCreatedEto eventData)
    {
        await _eventBus.PublishAsync(new EmbyShowAcceptedEto
        {
            ShowName = eventData.ShowName
        });
    }
}