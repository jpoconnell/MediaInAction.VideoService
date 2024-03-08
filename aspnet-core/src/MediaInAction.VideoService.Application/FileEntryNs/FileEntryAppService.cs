using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using MediaInAction.VideoService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.FileEntriesNs;

[Authorize(VideoServicePermissions.FileEntries.Default)]
public class FileEntryAppService : VideoServiceAppService, IFileEntryAppService
{
    private readonly FileEntryManager _fileEntryManager;
    private readonly IFileEntryRepository _fileEntryRepository;
    private readonly ILogger<FileEntryAppService> _logger;
    
    public FileEntryAppService(FileEntryManager movieManager,
        IFileEntryRepository fileEntryRepository,
        ILogger<FileEntryAppService> logger
    )
    {
        _fileEntryManager = movieManager;
        _fileEntryRepository = fileEntryRepository;
        _logger = logger;
    }
    
    public async Task<FileEntryDto> GetAsync(Guid id)
    {
        var fileEntry = await _fileEntryRepository.GetAsync(id);
        return null;
    }

    public async Task<FileEntryDto> CreateAsync(FileEntryCreatedDto input)
    {
        var fileEntry = await _fileEntryManager.CreateFileEntryAsync(
            externalId: new Guid(input.FileId),
            server: input.Server,
            directory: input.Directory,
            extn: input.Extn,
            size: input.Size,
            fileName: input.FileName,
            listName: input.ListName,
            sequence: input.Sequence,
            fileStatus: input.FileStatus
        );

        var fileEntryDto = new FileEntryDto();
        fileEntryDto.FileName = fileEntry.FileName;
        fileEntryDto.Server = fileEntry.Server;
        fileEntryDto.Directory = fileEntry.Directory;
        fileEntryDto.ListName = fileEntry.ListName;
        return fileEntryDto;
    }

    public async Task<FileEntryDto> GetFileEntryAsync(GetFileEntryInput input)
    {
        var fileEntry = await _fileEntryRepository.FindFileEntry(input.Server, input.Directory, 
            input.FileName, input.ListName);
        var fileEntryDto = new FileEntryDto
        {
            Id = fileEntry.Id,
            FileName = fileEntry.FileName,
            Server = fileEntry.Server,
            Directory = fileEntry.Directory,
            ListName = fileEntry.ListName
        };
        return fileEntryDto;
    }
    
    public Task SetAsMappedAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResultDto<FileEntryDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
    {
        throw new NotImplementedException();
    }

    public Task<DashboardDto> GetDashboardAsync(DashboardInput input)
    {
        throw new NotImplementedException();
    }
    
}
