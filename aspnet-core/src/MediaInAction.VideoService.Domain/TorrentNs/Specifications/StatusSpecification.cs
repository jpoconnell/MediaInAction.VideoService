using System;
using System.Linq.Expressions;
using MediaInAction.VideoService.Enums;
using MediaInAction.VideoService.TorrentsNs;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.TorrentNs.Specifications;

public class StatusSpecification : Specification<Torrent>
{
    protected FileStatus Status { get; set; }

    public StatusSpecification(FileStatus status)
    {
        Status = status;
    }

    public override Expression<Func<Torrent, bool>> ToExpression()
    {
        return query => query.TorrentStatus ==  Status;
    }
}