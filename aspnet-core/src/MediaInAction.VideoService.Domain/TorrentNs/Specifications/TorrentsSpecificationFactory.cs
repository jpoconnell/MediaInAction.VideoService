using System;
using MediaInAction.VideoService.Enums;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.TorrentsNs.Specifications;

public static class TorrentSpecificationFactory
{
    public static ISpecification<Torrent> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new Specifications.Last30DaysSpecification();
        }
        
        if (filter.StartsWith("st:"))
        {
            var status = int.Parse(filter.Split(':')[1]);
            return new Specifications.StatusSpecification((FileStatus) status);
        }
        
        return new  Specifications.Last30DaysSpecification();
    }
}