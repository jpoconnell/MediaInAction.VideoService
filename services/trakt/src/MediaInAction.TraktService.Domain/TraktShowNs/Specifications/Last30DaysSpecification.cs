using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktShowNs.Specifications;

public class Last30DaysSpecification : Specification<TraktShow>
{
    public override Expression<Func<TraktShow, bool>> ToExpression()
    {
        var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
        return query => query.CreationTime >= daysAgo30
            ;
    }
}