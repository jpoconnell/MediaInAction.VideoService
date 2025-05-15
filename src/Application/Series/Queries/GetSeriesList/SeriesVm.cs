using VideoService2.Application.Common.Models;

namespace VideoService2.Application.Series.Queries.GetSeriesList;

public class SeriesVm
{
    public IReadOnlyCollection<LookupDto> PriorityLevels { get; init; } = Array.Empty<LookupDto>();

    public IReadOnlyCollection<TodoListDto> Lists { get; init; } = Array.Empty<TodoListDto>();
}
