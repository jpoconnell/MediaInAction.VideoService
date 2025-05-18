using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mappergrpc;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;

using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;


namespace MediaInAction.FileService.FileEntriesNs;

public class FileEntryPublicService : IFileEntryPublicService, ITransientDependency
{
    private readonly IDistributedCache<FileEntryDto, Guid> _cache;
    private readonly ILogger<FileEntryPublicService> _logger;
    private readonly IObjectMapper _mapper;
    private readonly MapperGrpcService.MapperGrpcServiceClient _mapperGrpcServiceClient;
    private readonly GrpcChannel _channel;
    private readonly IFileEntryRepository _fileEntryRepository;
    private readonly FileEntryManager _fileEntryManager;

    public FileEntryPublicService(
        IDistributedCache<FileEntryDto, Guid> cache,
        ILogger<FileEntryPublicService> logger,
        FileEntryManager fileEntryManager,  
        IObjectMapper mapper,
        IFileEntryRepository fileEntryRepository,
        MapperGrpcService.MapperGrpcServiceClient mapperGrpcServiceClient)
    {
        _cache = cache;
        _mapper = mapper;
        _logger = logger;
        _fileEntryManager = fileEntryManager;
        _fileEntryRepository = fileEntryRepository;
        _channel = GrpcChannel.ForAddress("https://localhost:44356");
        _mapperGrpcServiceClient = new MapperGrpcService.MapperGrpcServiceClient(_channel);
    }

    public async Task<FileEntryDto> GetAsync(Guid fileEntryId)
    {
        return (await _cache.GetOrAddAsync(
            fileEntryId,
            () => GetOneFileEntryAsync(fileEntryId)
        ))!;
    }

    public async Task<FileEntryDto> FindByServerDirFileListAsync(string server, string directory, string filename, string extn, ListType listName)
    {
        try
        {
            var fileEntry =
                await _fileEntryRepository.GetByIdentifiers(server, directory, filename, extn, listName);
            if (fileEntry != null)
            {
                return MapToDto(fileEntry);
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    private FileEntryDto MapToDto(FileEntry fileEntry)
    {
        var fileEntryDto = new FileEntryDto();
        fileEntryDto.Server = fileEntry.Server;
        fileEntryDto.Directory = fileEntry.Directory;
        fileEntryDto.Filename = fileEntry.Filename;
        return fileEntryDto;
    }

    public async Task<List<FileEntryDto>> GetListAsync()
    {
        var fileEntries = await _fileEntryRepository.GetListAsync();
        return _mapper.Map<List<FileEntry>, List<FileEntryDto>>(fileEntries);
    }

    public async Task<FileEntryDto> CreateUpdateAsync(FileEntryCreateDto fileEntryCreateDto)
    {
        var fileEntry = await _fileEntryManager.CreateUpdateAsync(fileEntryCreateDto);
        if (fileEntry == null)
        {
            return null;
        }
        else
        {
            var fileEntryDto = MapToDto(fileEntry);
            return fileEntryDto;
        }
    }

    private async Task<FileEntryDto> GetOneFileEntryAsync(Guid fileEntryId)
    {
        var mapperModel = new MapperModel();
        
        var request = new SearchRequest();
        _logger.LogInformation("=== GRPC request {@request}", request);
        var response = await _mapperGrpcServiceClient.SearchOneMappedAsync(request);
        _logger.LogInformation("=== GRPC response {@response}", response);
        return _mapper.Map<MapperModel, FileEntryDto>(response) ??
               throw new UserFriendlyException(FileServiceDomainErrorCodes.FileEntryNotFound);
    }
}