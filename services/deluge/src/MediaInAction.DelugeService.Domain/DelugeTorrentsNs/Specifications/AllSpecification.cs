using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace  MediaInAction.DelugeService.DelugeTorrentNs.Specifications;

public class AllSpecification : Specification<DelugeTorrent>
{
    public override Expression<Func<DelugeTorrent, bool>> ToExpression()
    {
        return query => query.Name.Length > 0;
    }
}