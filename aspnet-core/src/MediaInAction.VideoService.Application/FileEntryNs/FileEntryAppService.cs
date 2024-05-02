﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using MediaInAction.VideoService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;
using DashboardDto = MediaInAction.VideoService.FileEntryNs.Dtos.DashboardDto;
using DashboardInput = MediaInAction.VideoService.FileEntryNs.Dtos.DashboardInput;

namespace MediaInAction.VideoService.FileEntryNs;

[Authorize(VideoServicePermissions.FileEntries.Default)]
public class FileEntryAppService : VideoServiceAppService, IFileEntryAppService
{
    private readonly FileEntryManager _fileEntryManager;
    private readonly IFileEntryRepository _fileEntryRepository;
    private readonly ILogger<FileEntryAppService> _logger;
    
    public FileEntryAppService(FileEntryManager fileEntryManager,
        IFileEntryRepository fileEntryRepository,
        ILogger<FileEntryAppService> logger
    )
    {
        _fileEntryManager = fileEntryManager;
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
        try
        {
            var fileEntry = await _fileEntryManager.CreateAsync(
                externalId: null,
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
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
        
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
    

    public async Task<PagedResultDto<FileEntryDto>> GetListPagedAsync(GetFileEntriesInput input)
    {
        ISpecification<FileEntry> specification = Specifications.SpecificationFactory.Create("a:");

        var fileEntryList =
            await _fileEntryRepository.GetListPagedAsync(specification, input.SkipCount,
                input.MaxResultCount, "FileName",true );

        var fileEntryDtoList = CreateFileEntryDtoMapping(fileEntryList);
        var totalCount = await _fileEntryRepository.GetCountAsync();
        return new PagedResultDto<FileEntryDto>(totalCount,fileEntryDtoList);
    }

    private List<FileEntryDto> CreateFileEntryDtoMapping(List<FileEntry> fileEntryList)
    {
        List<FileEntryDto> dtoList = new List<FileEntryDto>();
        foreach (var fileEntry in fileEntryList)
        {
            dtoList.Add(CreateFileEntryDtoMapping(fileEntry));
        }

        return dtoList;
    }

    public Task<DashboardDto> GetDashboardAsync(DashboardInput input)
    {
        throw new NotImplementedException();
    }
    
    private FileEntryDto CreateFileEntryDtoMapping(FileEntry fileEntry)
    {
        return new FileEntryDto()
        {
            Id = fileEntry.Id,
            FileName = fileEntry.FileName,
            Server = fileEntry.Server,
            Directory = fileEntry.Directory,
            ListName = fileEntry.ListName
        };
    }
}