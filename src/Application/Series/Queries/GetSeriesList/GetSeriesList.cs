using VideoService2.Application.Common.Interfaces;
using VideoService2.Application.Common.Models;
using VideoService2.Application.Common.Security;
using VideoService2.Domain.Enums;

namespace VideoService2.Application.Series.Queries.GetSeriesList;

[Authorize]
public record GetSeriesListQuery : IRequest<SeriesVm>;

public class GetSeriesListQueryHandler : IRequestHandler<GetSeriesListQuery, SeriesVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSeriesListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SeriesVm> Handle(GetSeriesListQuery request, CancellationToken cancellationToken)
    {
        return new SeriesVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new LookupDto { Id = (int)p, Title = p.ToString() })
                .ToList(),

            Lists = await _context.SeriesList
                .AsNoTracking()
                .ProjectTo<SeriesDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Name)
                .ToListAsync(cancellationToken)
        };
    }


}
