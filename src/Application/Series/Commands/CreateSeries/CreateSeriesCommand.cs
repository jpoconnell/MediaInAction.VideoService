using VideoService2.Application.Common.Interfaces;
using VideoService2.Domain.Entities;

namespace VideoService2.Application.Series.Commands.CreateSeries;

public record CreateSeriesCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateSeriesCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSeriesCommand request, CancellationToken cancellationToken)
    {
        var entity = new SeriesAlias();

        entity.IdType = request.Title;
        entity.IdValue = request.Title;
        _context.SeriesList.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
