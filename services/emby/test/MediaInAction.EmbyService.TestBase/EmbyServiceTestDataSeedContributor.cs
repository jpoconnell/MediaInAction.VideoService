using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeAliasesNs;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyEpisodesNs;
using MediaInAction.EmbyService.EmbyMovieAliasesNs;
using MediaInAction.EmbyService.EmbyMoviesNs;
using MediaInAction.EmbyService.EmbyShowAliasesNs;
using MediaInAction.EmbyService.EmbyShowsNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.EmbyService
{
    public class EmbyServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly EmbyShowManager _embyShowManager;
        private readonly EmbyMovieManager _embyMovieManager;
        private readonly EmbyEpisodeManager _embyEpisodeManager;
    
        private readonly TestData _testData;
        
        public EmbyServiceTestDataSeedContributor(
            EmbyShowManager showManager,
            EmbyMovieManager movieManager,
            EmbyEpisodeManager episodeManager,
            TestData testData)
        {
            _embyShowManager = showManager;
            _embyMovieManager = movieManager;
            _embyEpisodeManager = episodeManager;      
            _testData = testData;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
             await SeedTestEmbyServiceAsync();
      
        }

        private async Task SeedTestEmbyServiceAsync()
        {
            var embyShowAliasCreateList = new List<EmbyShowAliasCreateDto>();
            var embyShowAliasCreate = new EmbyShowAliasCreateDto
            {
                IdType = "_testData.ShowType1",
                IdValue = "_testData.ShowYear1"
            };
            embyShowAliasCreateList.Add(embyShowAliasCreate);
            
            var embyShowCreate = new EmbyShowCreateDto
            {
                Name = _testData.ShowName1,
                FirstAiredYear = _testData.ShowYear1,
                EmbyShowAliasesCreateDto = embyShowAliasCreateList
            };
            var newEmbyShow = await _embyShowManager.CreateAsync(embyShowCreate);
            
            // EmbyMovie
            var embyMovieAliasCreateDtoList = new List<EmbyMovieAliasCreateDto>();
            var embyMovieAliasCreateDto = new EmbyMovieAliasCreateDto
            {
                IdType = "_testData.MovieType1",
                IdValue = "_testData.MovieYear1"
            };
            embyMovieAliasCreateDtoList.Add(embyMovieAliasCreateDto);
            var embyMovieCreateDto = new EmbyMovieCreateDto
            {
                FirstAiredYear = _testData.MovieYear1,
                Name = _testData.MovieName1,
                EmbyMovieAliasCreateDtos = embyMovieAliasCreateDtoList
            };
            await _embyMovieManager.CreateAsync(embyMovieCreateDto);
            
            // EmbyEpisode
            var embyEpisodeAliasCreateDtoList = new List<EmbyEpisodeAliasCreateDto>();
            var embyEpisodeAliasCreateDto = new EmbyEpisodeAliasCreateDto
            {
                IdType = "_testData.MovieType1",
                IdValue = "_testData.MovieYear1"
            };
            embyEpisodeAliasCreateDtoList.Add(embyEpisodeAliasCreateDto);
            
            var embyEpisodeCreateDto = new EmbyEpisodeCreateDto
            {
                EmbySeriesId = newEmbyShow.Id,
                SeasonNum = _testData.SeasonNum1,
                EpisodeNum = _testData.EpisodeNume1,
                EmbyEpisodeAliasCreateDtos = embyEpisodeAliasCreateDtoList
            };
           
            await _embyEpisodeManager.CreateAsync(embyEpisodeCreateDto);
        }
    }
}