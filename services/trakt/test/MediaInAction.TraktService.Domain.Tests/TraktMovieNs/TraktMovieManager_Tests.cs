using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieManager_Tests : TraktServiceDomainTestBase
    {
        private readonly TraktMovieManager _traktMovieManager;
        private readonly ITraktMovieRepository _traktMovieRepository;
        private readonly TestData _testData;
        
        public TraktMovieManager_Tests()
        {
            _testData = GetRequiredService<TestData>();
            _traktMovieManager = GetRequiredService<TraktMovieManager>();
            _traktMovieRepository = GetRequiredService<ITraktMovieRepository>();
        }
        
        [Fact]
        public async Task Should_Update_Status()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                var movie = await _traktMovieRepository.GetListAsync();
                movie[0].Name = "FBI";
                await _traktMovieRepository.UpdateAsync(movie[0]);
            });

            var  dbShowList = await _traktMovieRepository.GetListAsync();
            var dbShow = dbShowList[0];
            dbShow.Name.ShouldBe("FBI");
        }

                
        [Fact]
        public async Task Should_Create_TraktMovie()
        {
            var newMovieAliases = new List<TraktMovieAliasCreateDto>();
            
            var newMovieAlias = new TraktMovieAliasCreateDto
            {
                IdType = "_testData.MovieAliasIdType1",
                IdValue = "_testData.MovieAliasIdValue1"
            };
            newMovieAliases.Add(newMovieAlias);
            
            var newMovie = new TraktMovieCreateDto();
            newMovie.Slug = _testData.Slug1;
            newMovie.Name = _testData.MovieName3;
            newMovie.FirstAiredYear = _testData.MovieYear1;
            newMovie.Status = TraktMovieStatus.New;
            newMovie.TraktMovieCreateAliases = newMovieAliases;
            var createdMovie = await _traktMovieManager.CreateAsync(newMovie);
            createdMovie.ShouldNotBeNull();
        }
    }
}
