using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyMoviesNs;

public interface IEmbyMovieRepository :  IRepository<EmbyMovie, Guid>
{
    Task<List<EmbyMovie>> GetListAsync(
        ISpecification<EmbyMovie> specification);
}