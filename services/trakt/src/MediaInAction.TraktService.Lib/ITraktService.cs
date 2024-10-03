using System.Threading.Tasks;

namespace MediaInAction.TraktService.Lib
{
    public interface ITraktService
    {
        Task SyncCalendarAsync();
        Task GetShowCollection();
        Task GetWatchedShows();
        Task GetWatchedMovies();
        Task GetMovieCollection();
        Task GetWatchedList(string shows);
        Task GetLastActivities();
    }
}
