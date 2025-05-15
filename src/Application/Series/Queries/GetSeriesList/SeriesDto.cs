namespace VideoService2.Application.Series.Queries.GetSeriesList;

public class SeriesDto
{
    public SeriesDto()
    {
        Items = Array.Empty<SeriesDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Colour { get; init; }

    public IReadOnlyCollection<SeriesDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Series, SeriesDto>();
        }
    }
}
