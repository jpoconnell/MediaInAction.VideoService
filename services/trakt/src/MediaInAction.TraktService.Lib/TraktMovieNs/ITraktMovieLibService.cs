using System.Threading.Tasks;
using MediaInAction.TraktService.Lib.TraktMovieNs.Dtos;

namespace MediaInAction.TraktService.Lib.TraktMovieNs;

public interface ITraktMovieLibService
    {
        Task UpdateAddFromDto(TraktCollectionMovieDto movie);
        
    }
