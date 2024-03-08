using System;
using System.Linq.Expressions;
using MediaInAction.VideoService.ToBeMappedNs;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.TorrentsNs.Specifications;

public class Last30DaysSpecification : Specification<Torrent>
{
    public override Expression<Func<Torrent, bool>> ToExpression()
    {
        var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
        return query => query.CreationTime >= daysAgo30
            ;
    }
}