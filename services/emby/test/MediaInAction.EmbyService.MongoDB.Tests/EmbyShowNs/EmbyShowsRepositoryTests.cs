using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowsNs;
using MediaInAction.EmbyService.MongoDB;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.EmbyShowNs;


[Collection(EmbyServiceTestConsts.CollectionDefinitionName)]
public class EmbyShowsRepositoryTests : EmbyServiceMongoDbTestBase
{
    private readonly IRepository<EmbyShow, Guid> _embyShowRepository;


    public EmbyShowsRepositoryTests()
    {
        _embyShowRepository = GetRequiredService<IRepository<EmbyShow, Guid> >();
    }

    [Fact]
    public async Task Should_Get_FirstShow()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var embyShow = await _embyShowRepository.FirstOrDefaultAsync();

            //Assert
            embyShow.ShouldNotBeNull();
        });
    }
    
    [Fact]
    public async Task Should_Get_ListOfShows()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var embyShowList = await _embyShowRepository.GetListAsync();

            //Assert
            embyShowList.Count.ShouldBeGreaterThan(0);
        });
    }
}
