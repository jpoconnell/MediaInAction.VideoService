namespace VideoService2.Application.SeriesAliases.Commands.UpdateSeriesAlias;

public class UpdateSeriesAliasCommandValidator : AbstractValidator<UpdateSeriesAliasCommand>
{
    public UpdateSeriesAliasCommandValidator()
    {
        RuleFor(v => v.IdType)
            .MaximumLength(200)
            .NotEmpty();
    }
}
