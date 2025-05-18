using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyMovieNs;

public interface IEmbyMovieRepository :  IRepository<EmbyMovie, Guid>
{
    Task<List<EmbyMovie>> GetListAsync(
        ISpecification<EmbyMovie> specification);

    Task<List<EmbyMovie>> GetListPagedAsync(
        ISpecification<EmbyMovie> specification, 
        int inputSkipCount, 
        int inputMaxResultCount, 
        string inputSorting);
}