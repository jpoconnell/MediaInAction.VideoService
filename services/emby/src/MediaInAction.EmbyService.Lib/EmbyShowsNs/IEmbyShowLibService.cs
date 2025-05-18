using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaInAction.EmbyService.EmbyShowsNs;

public interface IEmbyShowLibService
{
    Task UpdateAddFromDto(EmbyShowDto show);
    Task<List<EmbyShowDto>> GetAll();
    Task SendAllShowsEventList();
}
