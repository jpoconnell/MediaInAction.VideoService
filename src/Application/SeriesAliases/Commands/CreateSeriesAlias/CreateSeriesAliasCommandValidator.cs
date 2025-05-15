namespace VideoService2.Application.SeriesAliases.Commands.CreateSeriesAlias;

public class CreateSeriesAliasCommandValidator : AbstractValidator<CreateSeriesAliasCommand>
{
    public CreateSeriesAliasCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
