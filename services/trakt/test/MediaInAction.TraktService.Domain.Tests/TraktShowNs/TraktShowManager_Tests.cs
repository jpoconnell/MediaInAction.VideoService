using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.TraktShowNs
{
    public class TraktShowManager_Tests : TraktServiceDomainTestBase
    {
        private readonly TraktShowManager _traktShowManager;

        private readonly ITraktShowRepository _traktShowRepository;
        
        public TraktShowManager_Tests()
        {
            _traktShowManager = GetRequiredService<TraktShowManager>();
            _traktShowRepository = GetRequiredService<ITraktShowRepository>();
        }
        
        [Fact]
        public async Task Should_Set_Name()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                var show = await _traktShowRepository.FirstOrDefaultAsync();
                show.Name = "FBI";
                await _traktShowRepository.UpdateAsync(show);
            });

            var  dbShowList = await _traktShowRepository.GetListAsync();
            var dbShow = dbShowList[0];
            dbShow.Name.ShouldBe("FBI");
        }
    }
}
