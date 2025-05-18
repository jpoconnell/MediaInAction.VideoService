using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.FileEntriesNs;

public interface IFileEntryPublicService
{
    [ItemNotNull]
    Task<FileEntryDto> GetAsync(Guid episodeId);
    Task<List<FileEntryDto>> GetListAsync();
    
    Task<FileEntryDto> CreateUpdateAsync(FileEntryCreateDto traktEpisodeCreateDto);

    Task<FileEntryDto> FindByServerDirFileListAsync(string server, 
        string directory, 
        string filename, 
        string extn, 
        ListType listName);
}