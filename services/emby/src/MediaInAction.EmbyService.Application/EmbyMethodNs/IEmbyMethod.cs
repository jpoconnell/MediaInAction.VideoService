using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyRequestsNs;
using MediaInAction.EmbyService.EmbyRequestsNs.Dtos;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.EmbyService.EmbyMethodNs;

public interface IEmbyMethod : ITransientDependency
{
    string Name { get; }

    public Task<EmbyRequestStartResultDto> StartAsync(EmbyRequest2 embyRequest, EmbyRequestStartDto input);

    public Task<EmbyRequest2> CompleteAsync(IEmbyRequestRepository fileRequestRepository, string token);

    public Task HandleWebhookAsync(string payload);
}