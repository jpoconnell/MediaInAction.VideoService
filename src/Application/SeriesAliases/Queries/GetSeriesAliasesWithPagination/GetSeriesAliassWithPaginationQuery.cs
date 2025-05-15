using VideoService2.Application.Common.Interfaces;
using VideoService2.Application.Common.Models;

namespace VideoService2.Application.SeriesAliases.Queries.GetSeriesAliasesWithPagination;

public record GetSeriesAliassWithPaginationQuery : IRequest<PaginatedList<SeriesAliasBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetSeriesAliassWithPaginationQueryHandler : IRequestHandler<GetSeriesAliassWithPaginationQuery, PaginatedList<SeriesAliasBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSeriesAliassWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SeriesAliasBriefDto>> Handle(GetSeriesAliassWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.SeriesAliass
            .Where(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .ProjectTo<SeriesAliasBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
