using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyMovieNs.Specifications;

public class AllSpecification : Specification<EmbyMovie>
{
    
    public AllSpecification()
    {
    }

    public override Expression<Func<EmbyMovie, bool>> ToExpression()
    {
        return query => query.Name.Length >= 0;
    }
}