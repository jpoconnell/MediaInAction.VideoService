using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.DelugeTorrentsNs.Specifications;

public class Last30DaysSpecification : Specification<DelugeTorrent>
{
    public override Expression<Func<DelugeTorrent, bool>> ToExpression()
    {
        var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
        return query => query.CreationTime >= daysAgo30
            ;
    }
}
