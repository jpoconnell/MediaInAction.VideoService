using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService
{
    public class TraktServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly TraktShowManager _traktShowManager;
        private readonly TraktMovieManager _traktMovieManager;
        private readonly TraktEpisodeManager _traktEpisodeManager;
        private readonly TestData _testData;
        
        public TraktServiceTestDataSeedContributor(
            TraktShowManager traktShowManager,
            TraktMovieManager traktMovieManager,
            TraktEpisodeManager traktEpisodeManager,
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
            await SeedTestTraktServiceAsync();
            await SeedTestTraktEpisodeServiceAsync();
            return ;
        }

        private async Task SeedTestTraktServiceAsync()
        {
            //trak Show
            var traktShow1 = new TraktShowCreateDto
            {
                Slug = _testData.Slug1,
                Name = _testData.ShowName1,
                FirstAiredYear = _testData.ShowYear1,
                Status = TraktShowStatus.Cancelled,
                TraktShowCreateAliases = new List<TraktShowAliasCreateDto>()
                {
                    new TraktShowAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug1,
                    }
                }
            };
            var dbShow1 = await _traktShowManager.CreateAsync(traktShow1);
            
            var traktShow2 = new TraktShowCreateDto
            {
                Slug = _testData.Slug2,
                Name = _testData.ShowName2,
                FirstAiredYear = _testData.ShowYear2,
                Status = TraktShowStatus.New,
                TraktShowCreateAliases = new List<TraktShowAliasCreateDto>()
                {
                    new TraktShowAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var dbShow2 = await _traktShowManager.CreateAsync(traktShow2);
            
            var traktShow3 = new TraktShowCreateDto
            {
                Slug = _testData.Slug3,
                Name = _testData.ShowName3,
                FirstAiredYear = _testData.ShowYear3,
                Status = TraktShowStatus.New,
                TraktShowCreateAliases = new List<TraktShowAliasCreateDto>()
                {
                    new TraktShowAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var dbShow3 = await _traktShowManager.CreateAsync(traktShow3);
            
            //trakMovie
            var traktMovie1 = new TraktMovieCreateDto
            {
                Slug = _testData.Slug1,
                Name = _testData.MovieName1,
                FirstAiredYear = _testData.MovieYear1,
                Status = TraktMovieStatus.New,
                TraktMovieCreateAliases = new List<TraktMovieAliasCreateDto>()
                {
                    new TraktMovieAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug1,
                    }
                }
            };
            var dbMovie1 = await _traktMovieManager.CreateAsync(traktMovie1);
            
            var traktMovie2 = new TraktMovieCreateDto
            {
                Slug = _testData.Slug2,
                Name = _testData.MovieName2,
                FirstAiredYear = _testData.MovieYear2,
                Status = TraktMovieStatus.Cancelled,
                TraktMovieCreateAliases = new List<TraktMovieAliasCreateDto>()
                {
                    new TraktMovieAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var dbMovie2 = await _traktMovieManager.CreateAsync(traktMovie2);
            
            var traktMovie3 = new TraktMovieCreateDto
            {
                Slug = _testData.Slug3,
                Name = _testData.MovieName3,
                FirstAiredYear = _testData.MovieYear3,
                Status = TraktMovieStatus.Cancelled,
                TraktMovieCreateAliases = new List<TraktMovieAliasCreateDto>()
                {
                    new TraktMovieAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var dbMovie3 = await _traktMovieManager.CreateAsync(traktMovie3);
        }
        
        private async Task SeedTestTraktEpisodeServiceAsync()
        {
            //trakEpisode
            var traktEpisode1 = new TraktEpisodeCreateDto
            {
                Slug = _testData.Slug1,
                SeasonNum = _testData.SeasonNum1,
                EpisodeNum = _testData.EpisodeNum1,
                Status = TraktEpisodeStatus.New,
                TraktEpisodeCreateAliases = new List<TraktEpisodeAliasCreateDto>()
                {
                    new TraktEpisodeAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug1,
                    }
                }
            };
            var dbEpisode1 = await _traktEpisodeManager.CreateAsync(traktEpisode1);
            
            var traktEpisode2 = new TraktEpisodeCreateDto
            {
                Slug = _testData.Slug2,
                SeasonNum = _testData.SeasonNum1,
                EpisodeNum = _testData.EpisodeNum2,
                Status = TraktEpisodeStatus.Cancelled,
                TraktEpisodeCreateAliases = new List<TraktEpisodeAliasCreateDto>()
                {
                    new TraktEpisodeAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var dbEpisode2 = await _traktEpisodeManager.CreateAsync(traktEpisode2);
            
            var traktEpisode3 = new TraktEpisodeCreateDto
            {
                Slug = _testData.Slug2,
                SeasonNum = _testData.SeasonNum1,
                EpisodeNum = _testData.EpisodeNum3,
                Status = TraktEpisodeStatus.Cancelled,
                TraktEpisodeCreateAliases = new List<TraktEpisodeAliasCreateDto>()
                {
                    new TraktEpisodeAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var dbEpisode3 = await _traktEpisodeManager.CreateAsync(traktEpisode3);
        }
    }
}