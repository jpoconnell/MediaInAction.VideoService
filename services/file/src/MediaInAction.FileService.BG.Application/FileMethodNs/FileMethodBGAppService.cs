using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.FileService.FileMethodsNs;
using MediaInAction.FileService.FileMethodsNs.Dtos;


namespace MediaInAction.FileService.BG.FileMethodNs;

public class FileMethodBGAppService : FileServiceBGAppService, IFileMethodAppService
{
    private readonly IEnumerable<IFileMethod> _fileMethods;

    public FileMethodBGAppService(IEnumerable<IFileMethod> fileMethods)
    {
        this._fileMethods = fileMethods;
    }

    public Task<List<FileMethod>> GetListAsync()
    {
        return Task.FromResult(
                _fileMethods
                    .Select(p => new FileMethod { Name = p.Name })
                    .ToList()
            );
    }
}
