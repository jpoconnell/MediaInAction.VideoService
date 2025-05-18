using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService
{
    public class VideoServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly SeriesManager _seriesManager;
        private readonly MovieManager _movieManager;
        private readonly EpisodeManager _episodeManager;
        private readonly ToBeMappedManager _toBeMappedManager;
        private readonly TestData _testData;

        public VideoServiceTestDataSeedContributor(
            SeriesManager seriesManager,
            MovieManager movieManager,
            EpisodeManager episodeManager,
            ToBeMappedManager toBeMappedManager,
            TestData testData)
        {
            _seriesManager = seriesManager;
            _movieManager = movieManager;
            _toBeMappedManager = toBeMappedManager;
            _episodeManager = episodeManager;
            _testData = testData;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedTestVideoServiceAsync();
        }

        private async Task SeedTestVideoServiceAsync()
        {
            var series1 = new SeriesCreateDto
            {
                Name = _testData.SeriesName1,
                FirstAiredYear = _testData.SeriesYear1,
                SeriesAliasCreateDtos = new List<SeriesAliasCreateDto>()
                {
                    new SeriesAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug1,
                    }
                }
            };
            var series1Out = await _seriesManager.CreateUpdateAsync(series1);

            var series2 = new SeriesCreateDto
            {
                Name = _testData.SeriesName2,
                FirstAiredYear = _testData.SeriesYear2,
                SeriesAliasCreateDtos = new List<SeriesAliasCreateDto>()
                {
                    new SeriesAliasCreateDto()
                    {
                        IdType = "slug",
                        IdValue = _testData.Slug2,
                    }
                }
            };
            var series2Out = await _seriesManager.CreateUpdateAsync(series2);
            
            // Create Episodes
            var episode1 = new EpisodeCreateDto();
            episode1.SeriesId = series1Out.Id;
            episode1.SeasonNum = 1;
            episode1.EpisodeNum = 1;
            episode1.EpisodeName = _testData.EpisodeName1;
            episode1.EpisodeCreateAliases = new List<EpisodeAliasCreateDto>();
            episode1.EpisodeCreateAliases.Add(new EpisodeAliasCreateDto()
            {
                IdType = "Test product",
                IdValue = "Code:001"
            });

            await _episodeManager.CreateUpdateAsync(episode1);

            var episode2 = new EpisodeCreateDto();
            episode2.SeriesId = series1Out.Id;
            episode2.SeasonNum = 1;
            episode2.EpisodeNum = 2;
            episode2.EpisodeCreateAliases = new List<EpisodeAliasCreateDto>();
            episode2.EpisodeCreateAliases.Add(new EpisodeAliasCreateDto()
            {
                IdType = "Test name",
                IdValue = "Code:002"
            });

            await _episodeManager.CreateUpdateAsync(episode2);
   
            // Create Movies
            var movie1 = new MovieCreateDto();
            movie1.Name = _testData.MovieName1;
            movie1.FirstAiredYear = _testData.MovieYear1;
            movie1.MovieAliases = new List<MovieAliasCreateDto>();
            movie1.MovieAliases.Add(new MovieAliasCreateDto()
            {
                IdType = "Test product",
                IdValue = "Code:001"
            });
    
            await _movieManager.CreateUpdateAsync(movie1);
    
            var movie2 = new MovieCreateDto
            {
                Name = _testData.MovieName2,
                FirstAiredYear = _testData.MovieYear2,
                MovieAliases =
                [
                    new MovieAliasCreateDto()
                    {
                        IdType = "Test product",
                        IdValue = "Code:001"
                    }
                ]
            };

            await _movieManager.CreateUpdateAsync(movie2);
    
            // tobeMaopped
            
            var tpbemapped1 = new ToBeMappedCreateDto();
            tpbemapped1.Alias = "alias";
            await _toBeMappedManager.CreateAsync(tpbemapped1);
            var tpbemapped2 = new ToBeMappedCreateDto();
            tpbemapped2.Alias = "alias2";
            await _toBeMappedManager.CreateAsync(tpbemapped2);
    }
}
}