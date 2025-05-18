using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs;

public interface ISeriesAliasRepository : IRepository<SeriesAlias, Guid>
{
    Task<List<SeriesAlias>> GetByIdValue(string requestSlug);
    Task<SeriesAlias> FindByIdValue(string idValue);
}