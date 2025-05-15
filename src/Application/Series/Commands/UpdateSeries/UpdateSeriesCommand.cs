using VideoService2.Application.Common.Interfaces;

namespace VideoService2.Application.Series.Commands.UpdateSeries;

public record UpdateSeriesCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateSeriesCommandHandler : IRequestHandler<UpdateSeriesCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSeriesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateSeriesCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SeriesList
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound<int, Domain.Entities.Series>(request.Id, entity);

        entity.Name = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
