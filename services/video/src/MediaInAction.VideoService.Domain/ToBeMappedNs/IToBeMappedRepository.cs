using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.ToBeMappedNs
{
    public interface IToBeMappedRepository : IRepository<ToBeMapped, Guid>
    {
        Task<ToBeMapped> FindByAlias(string alias);
         
        Task<List<ToBeMapped>> GetToBeMappedsAsync(
            ISpecification<ToBeMapped> spec,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<List<ToBeMapped>> GetDashboardAsync(
            ISpecification<ToBeMapped> spec,
            CancellationToken cancellationToken = default);
    }
}