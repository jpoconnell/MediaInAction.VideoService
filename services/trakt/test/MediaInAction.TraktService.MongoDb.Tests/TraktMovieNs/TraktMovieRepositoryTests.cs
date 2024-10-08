﻿using System;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktMovieNs;
using MongoDB.Driver.Linq;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.MongoDb.TraktMovies;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(TraktServiceTestConsts.CollectionDefinitionName)]
public class TraktMovieRepositoryTests : TraktServiceMongoDbTestBase
{
    private readonly IRepository<TraktMovie, Guid> _traktMovieRepository;

    public TraktMovieRepositoryTests()
    {
        _traktMovieRepository = GetRequiredService<IRepository<TraktMovie, Guid>>();
    }

    [Fact]
    public async Task Should_Query_AppUser()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));     
                var adminUser = await (await _traktMovieRepository.GetMongoQueryableAsync())
                .FirstOrDefaultAsync();
                //Assert
                adminUser.ShouldNotBeNull();
        });
    }
}
