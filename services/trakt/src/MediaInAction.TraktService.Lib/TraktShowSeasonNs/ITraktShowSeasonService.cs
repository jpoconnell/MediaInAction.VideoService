using System.Threading.Tasks;

namespace MediaInAction.TraktService.Lib.TraktShowSeasonNs;
public interface ITraktShowSeasonService
{
    Task DoEpisodeCleanup();
}
