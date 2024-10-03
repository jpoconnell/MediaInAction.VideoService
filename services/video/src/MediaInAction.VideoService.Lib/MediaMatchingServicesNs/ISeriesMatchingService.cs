using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs;

namespace MediaInAction.VideoService.MediaMatchingServicesNs
{
    public interface ISeriesMatchingService
    {
        Task<bool> GetBySeriesName(string alias, List<SeriesAliasDto> seriesAliasList);
    }
}