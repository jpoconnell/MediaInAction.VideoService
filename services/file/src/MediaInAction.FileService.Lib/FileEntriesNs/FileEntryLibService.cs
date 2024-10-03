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
    private readonly IFileEntryRepository _fileEntryRepository;
    private readonly FileMappingManager _fileMappingManager;
    private readonly IFileMappingRepository _fileMappingRepository;
    
    public FileEntryLibService(
        FileEntryManager fileEntryManager,
        IFileEntryRepository fileEntryRepository,
        FileMappingManager fileMappingManager,
        IFileMappingRepository fileMappingRepository,
        ILogger<FileEntryLibService> logger)
    {
        _fileEntryManager = fileEntryManager;
        _fileEntryRepository = fileEntryRepository;
        _fileMappingManager = fileMappingManager;
        _fileMappingRepository = fileMappingRepository;
        _logger = logger;
    }
    
    public async Task CreateAsync(FileEntryCreateDto rec)
    {
        var fileEntry = await _fileEntryManager.CreateAsync(rec);
        await _fileMappingManager.CreateAsync(fileEntry);
    }
}
