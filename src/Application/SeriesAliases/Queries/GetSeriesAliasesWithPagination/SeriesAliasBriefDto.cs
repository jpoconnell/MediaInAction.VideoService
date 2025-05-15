using VideoService2.Domain.Entities;

namespace VideoService2.Application.SeriesAliases.Queries.GetSeriesAliasesWithPagination;

public class SeriesAliasBriefDto
{
    public int Id { get; init; }

    public string? IdType { get; init; }

    public string? IdValue { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SeriesAlias, SeriesAliasBriefDto>();
        }
    }
}
