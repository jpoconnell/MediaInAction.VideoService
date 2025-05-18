using System;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyMovieNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<EmbyMovie> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new AllSpecification();
        }

        if (filter.StartsWith("y"))
        {
            var year = int.Parse(filter.Split('y')[1]);
            return new YearSpecification(year);
        }
        
        return new AllSpecification();
    }
}