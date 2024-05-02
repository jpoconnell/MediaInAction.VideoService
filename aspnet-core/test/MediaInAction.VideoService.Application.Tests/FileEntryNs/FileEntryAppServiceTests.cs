using System.Threading.Tasks;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.FileEntryNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class FileEntryAppServiceTests<TStartupModule> : VideoServiceApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IFileEntryAppService _fileEntryAppService;

    protected FileEntryAppServiceTests()
    {
        _fileEntryAppService = GetRequiredService<IFileEntryAppService>();
    }

    [Fact]
    public async Task Initial_Data_Should_Contain_FileEntry()
    {
        //Act
        var filter = new GetFileEntriesInput();
        var result = await _fileEntryAppService.GetListPagedAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);

    }
}
