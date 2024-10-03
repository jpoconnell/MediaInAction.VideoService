using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileMethodsNs.Dtos;
using MediaInAction.FileService.FileRequestsNs;
using MediaInAction.FileService.FileRequestsNs.Dtos;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.BG.FileMethodNs;

[ExposeServices(typeof(IFileMethod), typeof(UnCompressMethod))]
public class UnCompressMethod : IFileMethod
{
   
    private readonly FileRequestDomainService _fileRequestDomainService;
    public string Name => FileMethodNames.UnCompress;
    
    public Task<FileRequestStartResultDto> StartAsync(FileRequest traktRequest, FileRequestStartDto input)
    {
        throw new NotImplementedException();
    }

    public Task<FileRequest> CompleteAsync(IFileRequestRepository traktRequestRepository, string token)
    {
        throw new NotImplementedException();
    }

    public Task HandleWebhookAsync(string payload)
    {
        throw new NotImplementedException();
    }
}