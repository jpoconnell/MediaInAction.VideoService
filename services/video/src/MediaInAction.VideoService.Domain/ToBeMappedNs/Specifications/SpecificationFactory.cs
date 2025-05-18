using System;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.ToBeMappedNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<ToBeMapped> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new Last30DaysSpecification();
        }
        
        
        return new Last30DaysSpecification();
    }
}