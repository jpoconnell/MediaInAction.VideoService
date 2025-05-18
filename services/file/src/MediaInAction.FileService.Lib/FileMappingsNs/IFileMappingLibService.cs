using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.FileService.FileMappingNs;

namespace MediaInAction.FileService.Lib.FileMappingsNs;

public interface IFileMappingLibService 
{
    Task<List<FileMapping>> GetUnMapped();
}

