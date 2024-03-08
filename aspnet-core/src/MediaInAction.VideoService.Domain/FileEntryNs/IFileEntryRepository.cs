using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.FileEntryNs;

public interface IFileEntryRepository : IRepository<FileEntry, Guid>
{
    Task<List<FileEntry>> GetFileEntriesByUserId(
        Guid userId,
        ISpecification<FileEntry> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<List<FileEntry>> GetFileEntriesAsync(
        ISpecification<FileEntry> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<List<FileEntry>> GetDashboardAsync(
        ISpecification<FileEntry> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<FileEntry> GetByFileEntryNoAsync(int fileEntryNo,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<FileEntry> FindFileEntry(string server, 
        string directory, 
        string fileName, 
        ListType listName);
    Task<List<FileEntry>> GetByLink(Guid episodeId);

    Task<List<FileEntry>> GetUnMapped();
    
    Task<List<FileEntry>> GetFileEntriesByUserId(
        Guid userId,
        ISpecification<FileEntry> spec,
        CancellationToken cancellationToken = default);
    
    Task<List<FileEntry>> GetMapped();
    Task<FileEntry> GetByExternalId(Guid fileId);
}