using VideoService2.Application.Common.Interfaces;

namespace VideoService2.Application.SeriesAliases.Commands.UpdateSeriesAlias;

public record UpdateSeriesAliasCommand : IRequest
{
    public int Id { get; init; }

    public string? IdType { get; init; }
    public string? IdValue { get; init; }
    
}

public class UpdateSeriesAliasCommandHandler : IRequestHandler<UpdateSeriesAliasCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSeriesAliasCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateSeriesAliasCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SeriesAliases
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.IdType = request.IdType;
        entity.IdValue = request.IdValue;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
