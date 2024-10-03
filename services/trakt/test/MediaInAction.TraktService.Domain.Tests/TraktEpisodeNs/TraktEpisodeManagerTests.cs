using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MediaInAction.TraktService.TraktEpisodeNs
{
    public class TraktEpisodeManagerTests : TraktServiceDomainTestBase
    {
        private readonly TraktEpisodeManager _traktEpisodeManager;
        private readonly ITraktEpisodeRepository _traktEpisodeRepository;
        private readonly TestData _testData;
        
        public TraktEpisodeManagerTests()
        {
            _testData = GetRequiredService<TestData>();
            _traktEpisodeManager = GetRequiredService<TraktEpisodeManager>();
            _traktEpisodeRepository = GetRequiredService<ITraktEpisodeRepository>();
        }
        
        [Fact]
        public async Task Should_Create_TraktEpisode()
        {
            var newEpisodeAliases = new List<TraktEpisodeAliasCreateDto>();
            
            var newEpisodeAlias = new TraktEpisodeAliasCreateDto
            {
                IdType = "_testData.EpisodeAliasIdType1",
                IdValue = "_testData.EpisodeAliasIdValue1"
            };
            newEpisodeAliases.Add(newEpisodeAlias);
            
            var newEpisode = new TraktEpisodeCreateDto();
            newEpisode.ShowSlug = _testData.ShowSlug1;
            newEpisode.SeasonNum = _testData.SeasonNum1;
            newEpisode.EpisodeNum = _testData.EpisodeNum1;
            newEpisode.AiredDate = DateTime.Now;
            newEpisode.TraktEpisodeCreateAliases = newEpisodeAliases;
            var createdEpisode = await _traktEpisodeManager.CreateAsync(newEpisode);
            createdEpisode.ShouldNotBeNull();
        }
    }
}
