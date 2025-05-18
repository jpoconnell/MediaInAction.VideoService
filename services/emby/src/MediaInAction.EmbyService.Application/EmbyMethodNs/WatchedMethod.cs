using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMethodsNs.Dtos;
using MediaInAction.EmbyService.EmbyRequestsNs;
using MediaInAction.EmbyService.EmbyRequestsNs.Dtos;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.EmbyService.EmbyMethodNs;

[ExposeServices(typeof(IEmbyMethod), typeof(WatchedMethod))]
public class WatchedMethod : IEmbyMethod
{
    public string Name => EmbyMethodNames.UnCompress;
    
    public Task<EmbyRequestStartResultDto> StartAsync(EmbyRequest embyRequest, EmbyRequestStartDto input)
    {
        throw new NotImplementedException();
    }

    public Task<EmbyRequest> CompleteAsync(IEmbyRequestRepository traktRequestRepository, string token)
    {
        throw new NotImplementedException();
    }

    public Task HandleWebhookAsync(string payload)
    {
        throw new NotImplementedException();
    }
}