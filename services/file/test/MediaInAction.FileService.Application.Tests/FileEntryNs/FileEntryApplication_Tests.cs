using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.FileService.FileEntryNs;

public class FileEntryApplication_Tests : FileServiceApplicationTestBase
{
    private readonly IFileEntryAppService _fileEntryAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public FileEntryApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _fileEntryAppService = GetRequiredService<IFileEntryAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

    [Fact]
    public async Task Should_Create_FileEntry()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
        // Create FileEntry
        var newFileEntry = new FileEntryCreateDto
        {
            Server = "local",
            FileName = "paypal",
            Directory = "/files"
        };
        var myFileEntry = await _fileEntryAppService.CreateAsync(newFileEntry);
        myFileEntry.ShouldNotBeNull();
    }
}