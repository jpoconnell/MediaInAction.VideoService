using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService
{
    public class VideoServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly SeriesManager _seriesManager;
        private readonly ISeriesRepository _seriesRepository;
        private readonly MovieManager _movieManager;
        private readonly EpisodeManager _episodeManager;
        private readonly ToBeMappedManager _toBeMappedManager;
        private readonly TestData _testData;
        
        public VideoServiceTestDataSeedContributor(
            SeriesManager seriesManager,
            ISeriesRepository seriesRepository,
            MovieManager movieManager,
            EpisodeManager episodeManager,
            ToBeMappedManager toBeMappedManager,
            TestData testData)
        {
            _seriesManager = seriesManager;
            _movieManager = movieManager;
            _seriesRepository = seriesRepository;
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
            await _seriesManager.CreateAsync(series1);
            
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
            var series2Out = await _seriesManager.CreateAsync(series2);
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
            await _movieManager.CreateAsync(movie1);

            var movie2 = new MovieCreateDto();
            movie2.Name = _testData.MovieName2;
            movie2.FirstAiredYear = _testData.MovieYear2;
            movie2.MovieAliases = new List<MovieAliasCreateDto>();
            movie2.MovieAliases.Add(new MovieAliasCreateDto()
            {
                IdType = "Test product",
                IdValue = "Code:001"
            });
            await _movieManager.CreateAsync(movie2);
            
            await _toBeMappedManager.CreateToBeMappedAsync(
                _testData.ToBeMapped1Alias
            );
            await _toBeMappedManager.CreateToBeMappedAsync(
                _testData.ToBeMapped2Alias
            );            
            await _toBeMappedManager.CreateToBeMappedAsync(
                "test alias 3"
            );

           var episode1 = new EpisodeCreateDto();
           episode1.SeriesId = series2Out.Id;
           episode1.SeasonNum = 1;
           episode1.EpisodeNum = 1;
           episode1.EpisodeCreateAliases = new List<EpisodeAliasCreateDto>();
           episode1.EpisodeCreateAliases.Add(new EpisodeAliasCreateDto()
           {
               IdType = "Test product",
               IdValue = "Code:001"
           });
              
           await _episodeManager.CreateAsync(episode1);
           
           var episode2 = new EpisodeCreateDto();
           episode2.SeriesId = series2Out.Id;
           episode2.SeasonNum = 1;
           episode2.EpisodeNum = 2;
           episode2.EpisodeCreateAliases = new List<EpisodeAliasCreateDto>();
           episode2.EpisodeCreateAliases.Add(new EpisodeAliasCreateDto()
           {
               IdType = "Test name",
               IdValue = "Code:002"
           });
              
           await _episodeManager.CreateAsync(episode2);
        }
    }
}