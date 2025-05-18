using System;
using System.Threading.Tasks;
using MediaInAction.TraktService.MongoDB;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.TraktShowNs;


[Collection(TraktServiceTestConsts.CollectionDefinitionName)]
public class TraktShowsRepositoryTests : TraktServiceMongoDbTestBase
{
    private readonly IRepository<TraktShow, Guid> _traktShowRepository;


    public TraktShowsRepositoryTests()
    {
        _traktShowRepository = GetRequiredService<IRepository<TraktShow, Guid> >();
    }

    [Fact]
    public async Task Should_Get_FirstShow()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var traktShow = await _traktShowRepository.FirstOrDefaultAsync();

            //Assert
            traktShow.ShouldNotBeNull();
        });
    }
    
    [Fact]
    public async Task Should_Get_ListOfShows()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var traktShowList = await _traktShowRepository.GetListAsync();

            //Assert
            traktShowList.Count.ShouldBeGreaterThan(0);
        });
    }
}
