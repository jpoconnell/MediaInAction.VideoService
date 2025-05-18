using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.EmbyShowNs;

    public class EmbyShowManagerTests : EmbyServiceDomainTestBase
    {
        private readonly EmbyShowManager _embyShowManager;
        private readonly IEmbyShowRepository _embyShowRepository;
        
        public EmbyShowManagerTests()
        {
            _embyShowManager = GetRequiredService<EmbyShowManager>();
            _embyShowRepository = GetRequiredService<IEmbyShowRepository>();
        }
        
    [Fact]
    public async Task Should_Set_Name()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var show = await _embyShowRepository.FirstOrDefaultAsync();
            show.Name = "FBI";
            await _embyShowRepository.UpdateAsync(show);
        });

        var  dbShowList = await _embyShowRepository.GetListAsync();
        var dbShow = dbShowList[0];
        dbShow.Name.ShouldBe("FBI");
    }

}
