using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktMovieNs.Specifications;

public class Last30DaysSpecification : Specification<TraktMovie>
{
    public override Expression<Func<TraktMovie, bool>> ToExpression()
    {
        var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
        return query => query.CreationTime >= daysAgo30
            ;
    }
}