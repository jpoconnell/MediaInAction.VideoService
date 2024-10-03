using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMethodsNs;
using MediaInAction.EmbyService.EmbyMethodsNs.Dtos;

namespace MediaInAction.EmbyService.EmbyMethodNs;

public class EmbyMethodAppService : EmbyServiceAppService, IEmbyMethodAppService
{
    private readonly IEnumerable<IEmbyMethod> _fileMethods;

    public EmbyMethodAppService(IEnumerable<IEmbyMethod> fileMethods)
    {
        this._fileMethods = fileMethods;
    }

    public Task<List<EmbyMethod>> GetListAsync()
    {
        return Task.FromResult(
                _fileMethods
                    .Select(p => new EmbyMethod { Name = p.Name })
                    .ToList()
            );
    }
}
