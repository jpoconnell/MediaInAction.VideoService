using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.FileService.FileEntriesNs
{
    public class FileEntryManager : DomainService
    {
        private readonly IFileEntryRepository _fileEntryRepository;
        private readonly ILogger<FileEntryManager> _logger;
        
        public FileEntryManager( 
            IFileEntryRepository fileEntryRepository, 
            ILogger<FileEntryManager> logger)
        {
            _fileEntryRepository = fileEntryRepository;
            _logger = logger;
        }

        public async Task<FileEntry> CreateAsync(FileEntryCreateDto input)
        {
            try
            {
                var fileEntry = new FileEntry(
                    GuidGenerator.Create(),
                    input.Server,
                    input.FileName,
                    input.Directory,
                    input.Directory,
                    input.Size,
                    input.Sequence,
                    ListType.Other,
                    FileStatus.New);
                var newFileEntry = await _fileEntryRepository.InsertAsync(fileEntry, true);
                _logger.LogInformation("File:" + input.FileName + " created successfully..!");
                return newFileEntry;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while creating file entry");
                throw;
            }
        }
  
        public async Task<FileEntry> CreateUpdateAsync(FileEntryCreateDto input)
        {
            try
            {
                var dbFileEntry = await _fileEntryRepository.GetByIdentifiers(input.Server,input.FileName,
                    input.Directory, input.Extn,input.ListName);

                if (dbFileEntry == null)
                {
                    var fileEntry = new FileEntry(
                        GuidGenerator.Create(),
                        input.Server,
                        input.FileName,
                        input.Directory,
                        input.Directory,
                        input.Size,
                        input.Sequence,
                        ListType.Other,
                        FileStatus.New);
                    var newFileEntry = await _fileEntryRepository.InsertAsync(fileEntry, true);
                    _logger.LogInformation("File:" + input.FileName + " created successfully..!");
                    return newFileEntry;
                }
                else  // update
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while creating file entry");
                throw;
            }
        }

        public async Task UpdateFileEntryStatus(Guid id,FileStatus status)
        {
            var fileEntry = await _fileEntryRepository.GetAsync(id);
            if (fileEntry.FileStatus != status)
            {
                fileEntry.FileStatus = status;
                await _fileEntryRepository.UpdateAsync(fileEntry, true);
            }
        }
    }
}