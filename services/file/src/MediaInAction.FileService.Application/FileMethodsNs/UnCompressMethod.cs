using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileMethodsNs.Dtos;
using MediaInAction.FileService.FileRequestsNs;
using MediaInAction.FileService.FileRequestsNs.Dtos;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.FileMethodsNs;

[ExposeServices(typeof(IFileMethod), typeof(UnCompressMethod))]
public class UnCompressMethod : IFileMethod
{
    public string Name => FileMethodNames.UnCompress;
    
    public Task<FileRequestStartResultDto> StartAsync(FileRequest traktRequest, FileRequestStartDto input)
    {
        return Task.FromResult(new FileRequestStartResultDto
        {
            CheckoutLink = input.ReturnUrl + "?token=" + input.FileRequestId
        });
    }

    public async Task<FileRequest> CompleteAsync(IFileRequestRepository fileRequestRepository, string token)
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