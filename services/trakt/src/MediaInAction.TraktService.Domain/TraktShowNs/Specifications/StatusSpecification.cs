using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktShowNs.Specifications;

public class StatusSpecification : Specification<TraktShow>
{
    protected TraktShowStatus ShowStatus { get; set; }

    public StatusSpecification(string status)
    {
        if (status == "Cancelled")
        {
            ShowStatus = TraktShowStatus.Cancelled;
        }
        if (status == "New")
        {
            ShowStatus = TraktShowStatus.New;
        }
    }

    public override Expression<Func<TraktShow, bool>> ToExpression()
    {
        return query => query.Status == ShowStatus;
    }
}