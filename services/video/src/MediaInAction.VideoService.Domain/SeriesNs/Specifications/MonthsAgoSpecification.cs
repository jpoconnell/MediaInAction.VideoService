using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs.Specifications;

public class MonthsAgoSpecification : Specification<Series>
{
    protected int NumberOfMonths { get; set; }

    public MonthsAgoSpecification(int months)
    {
        NumberOfMonths = months;
    }

    public override Expression<Func<Series, bool>> ToExpression()
    {
        var monthsAgo = DateTime.UtcNow.AddMonths(-NumberOfMonths);
        return query => query.CreationTime >= monthsAgo;
    }
}