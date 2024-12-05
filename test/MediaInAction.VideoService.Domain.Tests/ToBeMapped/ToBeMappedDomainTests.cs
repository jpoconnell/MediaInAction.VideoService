using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs;
using Shouldly;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.ToBeMapped;


public abstract class ToBeMappedDomainTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ToBeMappedManager _toBeMappedManager;

    protected ToBeMappedDomainTests()
    {
        _toBeMappedRepository = GetRequiredService<IToBeMappedRepository>();
        _toBeMappedManager = GetRequiredService<ToBeMappedManager>();
    }

    [Fact]
    public async Task ShouldCreateToBeMapped()
    {
        var toBeMappedCreatedDto = new ToBeMappedCreateDto();
        toBeMappedCreatedDto.Alias = "test";
        
        await WithUnitOfWorkAsync(async () =>
        {
            var toBeMapped = await _toBeMappedManager.CreateAsync(toBeMappedCreatedDto);
        });

        var dbToBeMapped = await _toBeMappedRepository.FindAsync(x => x.Alias == toBeMappedCreatedDto.Alias);
        dbToBeMapped.Alias.ShouldBe("test");
    }
}
