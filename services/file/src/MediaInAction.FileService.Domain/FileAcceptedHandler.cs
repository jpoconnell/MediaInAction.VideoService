using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace MediaInAction.FileService;

public class FileEntryAcceptedHandler : IDistributedEventHandler<FileEntryAcceptedEto>, ITransientDependency
{
    private readonly IFileEntryRepository _fileEntryRepository;

    public FileEntryAcceptedHandler(IFileEntryRepository fileEntryRepository)
    {
        _fileEntryRepository = fileEntryRepository;
    }
    
    [UnitOfWork]
    public virtual async Task HandleEventAsync(FileEntryAcceptedEto eventData)
    {

    }
}