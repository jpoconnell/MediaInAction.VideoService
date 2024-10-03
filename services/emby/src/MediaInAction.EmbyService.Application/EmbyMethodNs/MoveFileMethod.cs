using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMethodsNs.Dtos;
using MediaInAction.EmbyService.EmbyRequestsNs;
using MediaInAction.EmbyService.EmbyRequestsNs.Dtos;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.EmbyService.EmbyMethodNs;

[ExposeServices(typeof(IEmbyMethod), typeof(MoveEmbyMethod))]
public class MoveEmbyMethod : IEmbyMethod
{
    public string Name => EmbyMethodNames.Move;

    public Task<EmbyRequestStartResultDto> StartAsync(EmbyRequest2 embyRequest, EmbyRequestStartDto input)
    {
        
        return Task.FromResult(new EmbyRequestStartResultDto
        {
            CheckoutLink = input.ReturnUrl + "?token=" + input.EmbyRequestId
        });
    }

    public async Task<EmbyRequest2> CompleteAsync(IEmbyRequestRepository fileRequestRepository, string token)
    {
        var fileRequest = await fileRequestRepository.GetAsync(Guid.Parse(token));

        fileRequest.SetAsCompleted();

        return await fileRequestRepository.UpdateAsync(fileRequest);
    }

    public Task HandleWebhookAsync(string payload)
    {
        return Task.CompletedTask;
    }
}