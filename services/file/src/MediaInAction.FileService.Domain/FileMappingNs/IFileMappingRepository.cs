using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.FileService.FileMappingNs;

public interface IFileMappingRepository : IRepository<FileMapping, Guid>
{
    Task<List<FileMapping>> GetUnMapped();
}
