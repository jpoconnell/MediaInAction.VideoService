using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.SeriesNs;

public interface ISeriesAppService :
    ICrudAppService< //Defines CRUD methods
        SeriesDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateSeriesDto> //Used to create/update a book
{
    Task<object> GetDashboardAsync(DashboardInput dashboardInput);
    Task<SeriesDto> GetSeriessAsync(GetSeriessInput input);
}
