using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.MovieNs.Specifications;

public class ActiveSpecification : Specification<Movie>
{
    protected MovieStatus movieStatus { get; set; }
    
    public override Expression<Func<Movie, bool>> ToExpression()
    {
        return query => query.MovieStatus == movieStatus
            ;
        // && query.OrderDate <= DateTime.UtcNow;
    }
}