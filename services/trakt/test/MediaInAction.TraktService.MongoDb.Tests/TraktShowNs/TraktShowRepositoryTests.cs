using System;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs;
using MongoDB.Driver.Linq;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.MongoDb.TraktShowNs;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(TraktServiceTestConsts.CollectionDefinitionName)]
public class TraktShowRepositoryTests : TraktServiceMongoDbTestBase
{
    private readonly IRepository<TraktShow, Guid> _traktShowRepository;

    public TraktShowRepositoryTests()
    {
        _traktShowRepository = GetRequiredService<IRepository<TraktShow, Guid>>();
    }

    [Fact]
    public async Task Should_Query_()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));       
                var adminUser = await (await _traktShowRepository.GetMongoQueryableAsync())
                .FirstOrDefaultAsync();

                //Assert
                adminUser.ShouldNotBeNull();
        });
    }
}
