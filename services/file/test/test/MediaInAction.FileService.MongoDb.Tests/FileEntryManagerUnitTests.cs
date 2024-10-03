using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.FileService.MongoDB;
using Shouldly;
using Xunit;

namespace MediaInAction.FileService;

public class FileEntryManagerUnitTests : FileServiceMongoDbTestBase
{
    private readonly FileEntryManager _fileEntryManager;
    private readonly TestData _testData;
    
    public FileEntryManagerUnitTests()
    {
        _fileEntryManager = GetRequiredService<FileEntryManager>();
        _testData =  GetRequiredService<TestData>();
    }
    
    [Fact]
    public async Task Should_CreateFileEntryAsync()
    {
        var fileEntryToCreate = new FileEntryCreateDto();
        fileEntryToCreate.Server = _testData.Server;
        fileEntryToCreate.FileName = _testData.Filename3;
        fileEntryToCreate.Directory =_testData.Directory3;
        fileEntryToCreate.ListName = _testData.List3;
        var createdFileEntry = await _fileEntryManager.CreateAsync(
            fileEntryToCreate);
        
        createdFileEntry.ShouldNotBeNull();
    }
    /*
    [Fact]
    public async Task Should_Not_Create_Duplicate_FileEntryAsync()
    {
        var fileEntryToCreate = new FileEntryCreateDto();
        fileEntryToCreate.FileName = _testData.Filename1;
        fileEntryToCreate.Server = _testData.Server;
        fileEntryToCreate.Directory = _testData.Directory1;
        fileEntryToCreate.ListName = _testData.List1;
        fileEntryToCreate.Extn = _testData.Extn1;
        var createdFileEntry = await _fileEntryManager.CreateAsync(fileEntryToCreate);
        
        createdFileEntry.ShouldBeNull();
    }
    */
}