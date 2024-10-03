using System.Threading.Tasks;

namespace MediaInAction.EmbyService.EmbyMoviesNs;

public interface IEmbyMovieLibService
{
    Task UpdateAddFromDto(EmbyMovieDto movie);
}
