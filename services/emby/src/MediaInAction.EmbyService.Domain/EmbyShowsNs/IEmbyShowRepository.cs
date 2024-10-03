using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyShowsNs;

public interface IEmbyShowRepository :  IRepository<EmbyShow, Guid>
{
    Task<EmbyShow> GetByNameAsync(string showName);
    Task<List<EmbyShow>> GetListAsync(
        ISpecification<EmbyShow> specification);
    
    Task<List<EmbyShow>> GetListPagedAsync(
        ISpecification<EmbyShow> specification, 
        int inputSkipCount, 
        int inputMaxResultCount, 
        string inputSorting);

    Task<List<EmbyShow>> GetEmbyShowBySpec(ISpecification<EmbyShow> specification);
}