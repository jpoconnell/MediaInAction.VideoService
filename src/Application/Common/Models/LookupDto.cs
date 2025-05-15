using VideoService2.Domain.Entities;

namespace VideoService2.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Name { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
           // CreateMap<SeriesList, LookupDto>();
            CreateMap<SeriesAlias, LookupDto>();
        }
    }
}
