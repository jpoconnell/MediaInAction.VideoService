using System;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.DelugeTorrentNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<DelugeTorrent> Create(string filter)
    {
        if (filter.StartsWith("a:"))
        {
            return new AllSpecification();
        }
        
        if (filter.StartsWith("l:"))
        {
            return new Last30DaysSpecification();
        }

        if (filter.StartsWith("l:"))
        {
            var status = filter.Split(':')[1];
            return new LabelSpecification(status);
        }

        if (filter.IsNullOrEmpty())
        {
            return new AllSpecification();
        }
        return null;
    }
}