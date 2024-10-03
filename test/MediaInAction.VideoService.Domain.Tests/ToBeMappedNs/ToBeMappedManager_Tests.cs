using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.ToBeMappedNs;

public abstract class ToBeMappedManagerUnitTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ToBeMappedManager _toBeMaooedManager;

    public ToBeMappedManagerUnitTests()
    {
        _toBeMaooedManager = GetRequiredService<ToBeMappedManager>();
    }
    
    [Fact]
    public async Task Should_CreateToBeMappedAsync()
    {
        var alias = "Cash on Delivery";
        
        var createdToBeMapped = await _toBeMaooedManager.CreateToBeMappedAsync(
            alias);
        
        createdToBeMapped.ShouldNotBeNull();
        createdToBeMapped.Processed.ShouldBe(false);
    }
    
    [Fact]
    public async Task Should_Not_Create_Duplicate_ToBeMappedAsync()
    {
        var alias = "Cash on Delivery";
        
        var createdToBeMapped = await _toBeMaooedManager.CreateToBeMappedAsync(
            alias);
        
        createdToBeMapped.ShouldNotBeNull();
        createdToBeMapped.Processed.ShouldBe(false);
    }
}