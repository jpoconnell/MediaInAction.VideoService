using System;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktMovieNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<TraktMovie> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new Last30DaysSpecification();
        }
        
        if (filter.StartsWith("st:"))
        {
            var status = int.Parse(filter.Split(':')[1]);
            return new TraktMovieNs.Specifications.StatusSpecification(status);
        }
        
        return new Last30DaysSpecification();
    }
}