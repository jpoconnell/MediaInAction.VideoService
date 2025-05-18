using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMethodsNs.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService.EmbyMethodsNs;

public interface IEmbyMethodAppService : IApplicationService
{
    Task<List<EmbyMethod>> GetListAsync();
}
