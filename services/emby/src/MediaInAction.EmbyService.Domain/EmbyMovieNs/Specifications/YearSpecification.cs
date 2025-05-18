using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyMovieNs.Specifications;

public class YearSpecification : Specification<EmbyMovie>
{
    protected int Year { get; set; }

    public YearSpecification(int year)
    {
        Year = year;
    }

    public override Expression<Func<EmbyMovie, bool>> ToExpression()
    {
        return query => query.FirstAiredYear == Year;
    }
}