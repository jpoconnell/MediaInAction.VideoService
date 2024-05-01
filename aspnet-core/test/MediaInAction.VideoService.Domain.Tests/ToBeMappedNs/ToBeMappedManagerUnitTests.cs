using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.ToBeMappedNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IdentityUserManager here).
 * Only test your own domain services.
 */
public abstract class ToBeMappedManagerUnitTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ToBeMappedManager _toBeMappedManager;

    public ToBeMappedManagerUnitTests()
    {
        _toBeMappedRepository = GetRequiredService<IToBeMappedRepository>();
        _toBeMappedManager = GetRequiredService<ToBeMappedManager>();
    }

    [Fact]
    public async Task Should_CreateToBeMappedAsync()
    {
        var createdToBeMapped = await _toBeMappedManager.CreateAsync(
            "feederbox1"
        );
        
        createdToBeMapped.ShouldNotBeNull();
    }
}
