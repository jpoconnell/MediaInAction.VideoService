using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs.Specifications;

public class StatusSpecification : Specification<Episode>
{
    protected int EpisodeStatus { get; set; }

    public StatusSpecification(int status)
    {
        EpisodeStatus = status;
    }

    public override Expression<Func<Episode, bool>> ToExpression()
    {
        return query => query.EpisodeStatus == (EpisodeStatus) EpisodeStatus;
    }
}