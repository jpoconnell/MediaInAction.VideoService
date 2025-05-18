using System;
using System.Threading.Tasks;
using MediaInAction.FileService.MongoDB;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.FileService.FileEntriesNs;

[Collection(FileServiceTestConsts.CollectionDefinitionName)]
public class FileEntryRepositoryTests : FileServiceMongoDbTestBase
{
    private readonly IRepository<FileEntry, Guid> _fileEntryRepository;

    public FileEntryRepositoryTests()
    {
        _fileEntryRepository = GetRequiredService<IRepository<FileEntry, Guid>>();
    }
    
    
    [Fact]
    public async Task Should_Get_ListOfFileEntries()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var traktMovieList = await _fileEntryRepository.GetListAsync();

            //Assert
            traktMovieList.Count.ShouldBeGreaterThan(0);
        });
    }
}
