﻿using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs.Specifications;

public class Last30DaysSpecification : Specification<Series>
{
    public override Expression<Func<Series, bool>> ToExpression()
    {
        var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
        return query => query.CreationTime >= daysAgo30
            ;
        // && query.SeriesDate <= DateTime.UtcNow;
    }
}