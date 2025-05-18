using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs;

namespace MediaInAction.TraktService.Lib.TraktShowNs;

public interface ITraktShowLibService
{
    Task UpdateAddFromDto(TraktShowCreateDto show);
    Task ResendUnAcceptedShowsList();
}
