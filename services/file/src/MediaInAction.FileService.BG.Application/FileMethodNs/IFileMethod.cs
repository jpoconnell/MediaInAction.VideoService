using System.Threading.Tasks;
using MediaInAction.FileService.FileRequestsNs;
using MediaInAction.FileService.FileRequestsNs.Dtos;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.BG.FileMethodNs;

public interface IFileMethod : ITransientDependency
{
    string Name { get; }

    public Task<FileRequestStartResultDto> StartAsync(FileRequest fileRequest, FileRequestStartDto input);

    public Task<FileRequest> CompleteAsync(IFileRequestRepository fileRequestRepository, string token);

    public Task HandleWebhookAsync(string payload);
}