using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.FileService.FileMappingNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.Lib.FileMappingsNs;

public class FileMappingLibService : IFileMappingLibService, ITransientDependency
{
    private readonly ILogger<FileMappingLibService> _logger;
    private readonly IFileMappingRepository _fileMappingRepository;
    
    public FileMappingLibService(
        IFileMappingRepository fileMappingRepository,
        ILogger<FileMappingLibService> logger)
    {
        _fileMappingRepository = fileMappingRepository;
        _logger = logger;
    }

    public async Task<List<FileMapping>> GetUnMapped()
    {
        var unMappedList = await _fileMappingRepository.GetUnMapped();
        return unMappedList;
    }
}
