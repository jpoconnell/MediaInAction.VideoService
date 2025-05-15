using VideoService2.Application.Common.Interfaces;

namespace VideoService2.Application.Series.Commands.UpdateSeries;

public class UpdateSeriesCommandValidator : AbstractValidator<UpdateSeriesCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSeriesCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(UpdateSeriesCommand model, string title, CancellationToken cancellationToken)
    {
        return !await _context.SeriesList
            .Where(l => l.Id != model.Id)
            .AnyAsync(l => l.Title == title, cancellationToken);
    }
}
