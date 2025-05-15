using VideoService2.Application.Common.Interfaces;

namespace VideoService2.Application.Series.Commands.DeleteSeries;

public record DeleteSeriesCommand(int Id) : IRequest;

public class DeleteSeriesCommandHandler : IRequestHandler<DeleteSeriesCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSeriesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSeriesCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SeriesList
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.SeriesList.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
