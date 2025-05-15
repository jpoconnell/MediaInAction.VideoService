using VideoService2.Application.Common.Interfaces;
using VideoService2.Domain.Entities;

namespace VideoService2.Application.SeriesAliases.Commands.CreateSeriesAlias;

public record CreateSeriesAliasCommand : IRequest<int>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class CreateSeriesAliasCommandHandler : IRequestHandler<CreateSeriesAliasCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateSeriesAliasCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSeriesAliasCommand request, CancellationToken cancellationToken)
    {
        var entity = new SeriesAlias
        {
            IdValue = request.ListId,
            IdType = request.Title
        };

        entity.AddDomainEvent(new SeriesAliasCreatedEvent(entity));

        _context.SeriesAliases.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
