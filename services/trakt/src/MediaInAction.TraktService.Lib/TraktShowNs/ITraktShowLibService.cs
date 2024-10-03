using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.Lib.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktShowNs;

namespace MediaInAction.TraktService.Lib.TraktShowNs;

public interface ITraktShowLibService
{
    Task UpdateAddFromDto(CollectionShowDto show);
    Task<List<TraktShowDto>> GetShows();

    Task<List<TraktShowDto>> GetChangedShows();

    Task ResendUnAcceptedShowsList();
}
