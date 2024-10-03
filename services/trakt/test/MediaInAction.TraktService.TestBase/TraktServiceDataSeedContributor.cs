using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace MediaInAction.TraktService
{
    public class TraktServiceDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly TraktShowManager _showManager;
        private readonly ITraktShowRepository _traktShowRepository;
        private readonly TraktMovieManager _movieManager;
        private readonly ITraktMovieRepository _movieRepository;
        private readonly TraktEpisodeManager _episodeManager;
        private readonly ITraktEpisodeRepository _episodeRepository;
        private readonly TestData _testData;
        
        public TraktServiceDataSeedContributor(
            IGuidGenerator guidGenerator, ICurrentTenant currentTenant,
            TraktShowManager showManager,
            ITraktShowRepository traktShowRepository,
            TraktMovieManager movieManager,
            ITraktMovieRepository movieRepository,
            TraktEpisodeManager episodeManager,
            ITraktEpisodeRepository episodeRepository,
            TestData testData)
        {
            _showManager = showManager;
            _traktShowRepository = traktShowRepository;
            _movieManager = movieManager;
            _movieRepository = movieRepository;
            _episodeManager = episodeManager;
            _episodeRepository = episodeRepository;
            _testData = testData;
        }
        
        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedTestTraktServiceAsync();
        }
        private async Task SeedTestTraktServiceAsync()
        {
            // TraktShow
            var traktShowAliasCreateList = new List<TraktShowAliasCreateDto>();
            var traktShowAliasCreate= new TraktShowAliasCreateDto
            {
                IdType = "_testData.ShowType1",
                IdValue = "_testData.ShowYear1"
            };
            traktShowAliasCreateList.Add(traktShowAliasCreate);
            
            var traktShowCreate = new TraktShowCreateDto
            {
                Name = _testData.ShowName1,
                FirstAiredYear = _testData.ShowYear1,
                Slug = _testData.ShowSlug1,
                TraktShowCreateAliases = traktShowAliasCreateList
            };
            var newSeries = await _showManager.CreateAsync(traktShowCreate);
            
            // TraktMovie
            var traktMovieAliasCreateDtoList = new List<TraktMovieAliasCreateDto>();
            var traktMovieAliasCreateDto = new TraktMovieAliasCreateDto
            {
                IdType = "_testData.MovieType1",
                IdValue = "_testData.MovieYear1"
            };
            traktMovieAliasCreateDtoList.Add(traktMovieAliasCreateDto);
            var traktMovieCreateDto = new TraktMovieCreateDto
            {
                FirstAiredYear = _testData.MovieYear1,
                Name = _testData.MovieName1,
                Slug = _testData.MovieSlug1,
                TraktMovieCreateAliases = traktMovieAliasCreateDtoList
            };
            await _movieManager.CreateAsync(traktMovieCreateDto);
            
            // TraktEpisode
            var traktEpisodeAliasCreateDtoList = new List<TraktEpisodeAliasCreateDto>();
            var traktEpisodeAliasCreateDto = new TraktEpisodeAliasCreateDto
            {
                IdType = "_testData.MovieType1",
                IdValue = "_testData.MovieYear1"
            };
            traktEpisodeAliasCreateDtoList.Add(traktEpisodeAliasCreateDto);
            
            var traktEpisodeCreateDto = new TraktEpisodeCreateDto
            {
                ShowSlug = _testData.ShowSlug1,
                SeasonNum = _testData.SeasonNum1,
                EpisodeNum = _testData.EpisodeNum1,
                TraktEpisodeCreateAliases = traktEpisodeAliasCreateDtoList
            };
           
            await _episodeManager.CreateAsync(traktEpisodeCreateDto);
        }
    }
}
