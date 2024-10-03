using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.EmbyEpisodesNs
{
    public class EmbyEpisodeManagerTests : EmbyServiceDomainTestBase
    {
        private readonly EmbyEpisodeManager _embyEpisodeManager;
        private readonly IEmbyEpisodeRepository _embyEpisodeRepository;
        
        public EmbyEpisodeManagerTests()
        {
            _embyEpisodeManager = GetRequiredService<EmbyEpisodeManager>();
            _embyEpisodeRepository = GetRequiredService<IEmbyEpisodeRepository>();
        }

        [Fact]
        public async Task Should_Set_Season_Of_Emby_Episode()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                var episode = await _embyEpisodeRepository.FirstOrDefaultAsync();
                episode.SeasonNum = 1;
                await _embyEpisodeRepository.UpdateAsync(episode);
            });

           var  dbEpisodeList = await _embyEpisodeRepository.GetListAsync();
           var dbEpisode = dbEpisodeList[0];
           dbEpisode.SeasonNum.ShouldBe(1);
        }
    }
}
