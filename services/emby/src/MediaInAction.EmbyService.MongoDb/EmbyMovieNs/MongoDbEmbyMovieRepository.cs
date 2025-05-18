using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyMovieNs;

public class MongoDbEmbyMovieRepository
    : MongoDbRepository<EmbyServiceMongoDbContext, EmbyMovie, Guid>,
    IEmbyMovieRepository
{
    public MongoDbEmbyMovieRepository(
        IMongoDbContextProvider<EmbyServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }

    public Task<List<EmbyMovie>> GetListAsync(ISpecification<EmbyMovie> specification)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyMovie>> GetListPagedAsync(
        ISpecification<EmbyMovie> specification, 
        int inputSkipCount, 
        int inputMaxResultCount, 
        string inputSorting)
    {
        throw new NotImplementedException();
    }
}
