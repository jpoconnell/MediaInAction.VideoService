using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.FileEntryNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IdentityUserManager here).
 * Only test your own domain services.
 */
public abstract class FileEntryManagerUnitTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IFileEntryRepository _fileEntryRepository;
    private readonly FileEntryManager _fileEntryManager;

    public FileEntryManagerUnitTests()
    {
        _fileEntryRepository = GetRequiredService<IFileEntryRepository>();
        _fileEntryManager = GetRequiredService<FileEntryManager>();
    }


    [Fact]
    public async Task Should_CreateFileEntryAsync()
    {
        var createdFileEntry = await _fileEntryManager.CreateAsync(
            Guid.NewGuid(), 
            "feederbox1",
            "/etc/feeds",
            "mkv",
            20,
            "testfile",
            ListType.Torrent,
            1,
            FileStatus.New,
            true
        );
        
        createdFileEntry.ShouldNotBeNull();
    }
}
