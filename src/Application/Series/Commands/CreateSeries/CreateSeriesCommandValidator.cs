using VideoService2.Application.Common.Interfaces;

namespace VideoService2.Application.Series.Commands.CreateSeries;

public class CreateSeriesCommandValidator : AbstractValidator<CreateSeriesCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateSeriesCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return !await _context.SeriesList
            .AnyAsync(l => l.Title == title, cancellationToken);
    }
}
