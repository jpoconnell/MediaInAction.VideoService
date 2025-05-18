using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMovieNs;

namespace MediaInAction.EmbyService.EmbyMoviesNs;

public interface IEmbyMovieLibService
{
    Task UpdateAddFromDto(EmbyMovieDto movie);
}
