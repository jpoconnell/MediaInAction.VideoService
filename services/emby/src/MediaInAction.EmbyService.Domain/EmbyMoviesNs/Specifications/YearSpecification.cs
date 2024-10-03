using System;
using System.Linq.Expressions;
using MediaInAction.EmbyService.EmbyShowsNs;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyMoviesNs.Specifications;

public class YearSpecification : Specification<EmbyShow>
{
    protected int Year { get; set; }

    public YearSpecification(int year)
    {
        Year = year;
    }

    public override Expression<Func<EmbyShow, bool>> ToExpression()
    {
        return query => query.FirstAiredYear == Year;
    }
}