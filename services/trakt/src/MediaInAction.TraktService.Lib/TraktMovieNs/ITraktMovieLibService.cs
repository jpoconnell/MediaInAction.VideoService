using System.Threading.Tasks;
using MediaInAction.TraktService.TraktMovieNs;

namespace MediaInAction.TraktService.Lib.TraktMovieNs;

public interface ITraktMovieLibService
{
    Task UpdateAddFromDto(TraktMovieCreateDto traktMovieCreateDto);
    
}
