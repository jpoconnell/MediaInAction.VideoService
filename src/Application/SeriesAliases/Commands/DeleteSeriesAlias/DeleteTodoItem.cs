using VideoService2.Application.Common.Interfaces;

namespace VideoService2.Application.SeriesAliases.Commands.DeleteSeriesAlias;

public record DeleteSeriesAliasCommand(int Id) : IRequest;

public class DeleteSeriesAliasCommandHandler : IRequestHandler<DeleteSeriesAliasCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSeriesAliasCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSeriesAliasCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SeriesAliases
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.SeriesAliases.Remove(entity);

        entity.AddDomainEvent(new SeriesAliasDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
