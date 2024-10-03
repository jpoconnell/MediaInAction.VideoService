using System;
using MediaInAction.EmbyService.EmbyShowsNs;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyMoviesNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<EmbyShow> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new EmbyShowsNs.Specifications.AllSpecification();
        }

        if (filter.StartsWith("y"))
        {
            var year = int.Parse(filter.Split('y')[1]);
            return new YearSpecification(year);
        }
        
        return new EmbyShowsNs.Specifications.AllSpecification();
    }
}