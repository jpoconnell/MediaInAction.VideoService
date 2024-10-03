using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.DelugeTorrentNs.Specifications;

public class LabelSpecification : Specification<DelugeTorrent>
{
    protected string Label { get; set; }

    public LabelSpecification(string status)
    {
        Label = status;
    }

    public override Expression<Func<DelugeTorrent, bool>> ToExpression()
    {
        return query => query.Label ==  Label;
    }
}