using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeAliasNs;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyMovieAliasNs;
using MediaInAction.EmbyService.EmbyMovieNs;
using MediaInAction.EmbyService.EmbyShowAliasesNs;
using MediaInAction.EmbyService.EmbyShowNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.EmbyService
{
    public class EmbyServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly EmbyShowManager _traktShowManager;
        private readonly EmbyMovieManager _traktMovieManager;
        private readonly EmbyEpisodeManager _traktEpisodeManager;
        private readonly TestData _testData;
        
        public EmbyServiceTestDataSeedContributor(
            EmbyShowManager traktShowManager,
            EmbyMovieManager traktMovieManager,
            EmbyEpisodeManager traktEpisodeManager,
            TestData testData)
        {
            _traktShowManager = traktShowManager;
            _traktMovieManager = traktMovieManager;
            _traktEpisodeManager = traktEpisodeManager;
            _testData = testData;
        }
        
        public async Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */
            await SeedTestEmbyServiceAsync();
            await SeedTestEmbyEpisodeServiceAsync();
            return ;
        }


        private async Task SeedTestEmbyServiceAsync()
        {
            //trak Show
            var traktShow1 = new EmbyShowCreateDto
            {
                Name = _testData.ShowName1,
                FirstAiredYear = _testData.ShowYear1,
                Status = EmbyShowStatus.Cancelled,
                EmbyShowCreateAliases = new List<EmbyShowAliasCreateDto>()
                {
                    new EmbyShowAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug1,
                    }
                }
            };
            var dbShow1 = await _traktShowManager.CreateAsync(traktShow1);
            
            var traktShow2 = new EmbyShowCreateDto
            {
                Name = _testData.ShowName2,
                FirstAiredYear = _testData.ShowYear2,
                Status = EmbyShowStatus.Cancelled,
                EmbyShowCreateAliases = new List<EmbyShowAliasCreateDto>()
                {
                    new EmbyShowAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var dbShow2 = await _traktShowManager.CreateAsync(traktShow2);
            
            //trakMovie
            var traktMovie1 = new EmbyMovieCreateDto
            {
                Name = _testData.MovieName1,
                FirstAiredYear = _testData.MovieYear1,
                Status = EmbyMovieStatus.New,
                EmbyMovieCreateAliasesDto = new List<EmbyMovieAliasCreateDto>()
                {
                    new EmbyMovieAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug1,
                    }
                }
            };
            var dbMovie1 = await _traktMovieManager.CreateAsync(traktMovie1);
            
            var traktMovie2 = new EmbyMovieCreateDto
            {
                Name = _testData.MovieName2,
                FirstAiredYear = _testData.MovieYear2,
                Status = EmbyMovieStatus.New,
                EmbyMovieCreateAliasesDto = new List<EmbyMovieAliasCreateDto>()
                {
                    new EmbyMovieAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var dbMovie2 = await _traktMovieManager.CreateAsync(traktMovie2);
        }
        
        private async Task SeedTestEmbyEpisodeServiceAsync()
        {
            //trakEpisode
            var traktEpisode1 = new EmbyEpisodeCreateDto
            {
                SeasonNum = _testData.SeasonNum1,
                EpisodeNum = _testData.EpisodeNume1,
                Status = EmbyEpisodeStatus.New,
                EmbyEpisodeAliasCreateDto = new List<EmbyEpisodeAliasCreateDto>()
                {
                    new EmbyEpisodeAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug1,
                    }
                }
            };
            var dbEpisode1 = await _traktEpisodeManager.CreateAsync(traktEpisode1);
            
            //trakEpisode
            var traktEpisode2 = new EmbyEpisodeCreateDto
            {
                SeasonNum = _testData.SeasonNum1,
                EpisodeNum = _testData.EpisodeNume2,
                Status = EmbyEpisodeStatus.New,
                EmbyEpisodeAliasCreateDto = new List<EmbyEpisodeAliasCreateDto>()
                {
                    new EmbyEpisodeAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var dbEpisode2 = await _traktEpisodeManager.CreateAsync(traktEpisode2);
        }
    }
}