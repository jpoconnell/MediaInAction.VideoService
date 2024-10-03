using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktShowNs;

public interface ITraktShowRepository :  IRepository<TraktShow, Guid>
{
    Task<TraktShow> FindByTraktShowNameYearAsync(string name,int year);
    
    Task<TraktShow> FindBySlug(
        [NotNull] string showSlug,
        bool includeDetails = true,
        CancellationToken cancellationToken = default
    );
    Task<List<TraktShow>> GetDashboardAsync(ISpecification<TraktShow> spec);
    Task<List<TraktShow>> GetTraktShowBySpec(ISpecification<TraktShow> spec, bool b);
    Task<List<TraktShow>> GetChangedListAsync();
    Task<TraktShow> FindSlugAsync(string slug);
}