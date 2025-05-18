using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.FileService.FileMappingNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.Lib.FileEntriesNs;

public class FileEntryLibService : IFileEntryLibService, ITransientDependency
{
    private readonly ILogger<FileEntryLibService> _logger;
    private readonly FileEntryManager _fileEntryManager;
    private readonly FileMappingManager _fileMappingManager;
    
    public FileEntryLibService(
        FileEntryManager fileEntryManager,
        IFileEntryRepository fileEntryRepository,
        FileMappingManager fileMappingManager,
        IFileMappingRepository fileMappingRepository,
        ILogger<FileEntryLibService> logger)
    {
        _fileEntryManager = fileEntryManager;
        _fileMappingManager = fileMappingManager;
        _logger = logger;
    }
    
    public async Task CreateAsync(FileEntryCreateDto rec)
    {
        var fileEntry = await _fileEntryManager.CreateAsync(rec);
        await _fileMappingManager.CreateAsync(fileEntry);
    }
}
