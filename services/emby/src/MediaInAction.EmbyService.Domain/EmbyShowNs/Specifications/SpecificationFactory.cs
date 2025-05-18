using System;
using MediaInAction.EmbyService.EmbyShowsNs.Specifications;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyShowNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<EmbyShow> Create(string filter)
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