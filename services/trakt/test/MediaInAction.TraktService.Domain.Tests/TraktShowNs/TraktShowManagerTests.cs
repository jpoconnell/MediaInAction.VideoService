using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace MediaInAction.TraktService.TraktShowNs;

    public class TraktShowManagerTests : TraktServiceDomainTestBase
    {
        private readonly TraktShowManager _traktShowManager;
        private readonly ITraktShowRepository _traktShowRepository;
        private readonly TestData _testData;

        public TraktShowManagerTests()
        {
            _testData = GetRequiredService<TestData>();
            _traktShowManager = GetRequiredService<TraktShowManager>();
            _traktShowRepository = GetRequiredService<ITraktShowRepository>();
        }
        
    [Fact]
    public async Task Should_Set_Name()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var show = await _traktShowRepository.GetListAsync();
            show[0].Name = "FBI";
            await _traktShowRepository.UpdateAsync(show[0]);
        });

        var  dbShowList = await _traktShowRepository.GetListAsync();
        var dbShow = dbShowList[0];
        dbShow.Name.ShouldBe("FBI");
    }

    [Fact]
    public async Task Should_Create_TraktShow()
    {
        var newShowAliases = new List<TraktShowAliasCreateDto>();
            
        var newShowAlias = new TraktShowAliasCreateDto
        {
            IdType = "_testData.ShowAliasIdType1",
            IdValue = "_testData.ShowAliasIdValue1"
        };
        newShowAliases.Add(newShowAlias);
            
        var newShow = new TraktShowCreateDto();
        newShow.Slug = _testData.Slug1;
        newShow.Name = _testData.ShowName3;
        newShow.FirstAiredYear = _testData.ShowYear1;
        newShow.Status = TraktShowStatus.New;
        newShow.TraktShowCreateAliases = newShowAliases;
        var createdShow = await _traktShowManager.CreateAsync(newShow);
        createdShow.ShouldNotBeNull();
    }
}
