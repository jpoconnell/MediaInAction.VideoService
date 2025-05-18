using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktMovieNs.Specifications;

public class StatusSpecification : Specification<TraktMovie>
{
    protected int MovieStatus { get; set; }

    public StatusSpecification(int status)
    {
        MovieStatus = status;
    }

    public override Expression<Func<TraktMovie, bool>> ToExpression()
    {
        return query => query.Status == (TraktMovieStatus) MovieStatus;
    }
}