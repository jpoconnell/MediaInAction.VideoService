using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.FileService.FileEntryNs
{
    public class FileEntryManager_Tests : FileServiceDomainTestBase
    {

        private readonly IFileEntryRepository _fileEntryRepository;
        
        public FileEntryManager_Tests()
        {
            _fileEntryRepository = GetRequiredService<IFileEntryRepository>();
        }
        
        [Fact]
        public async Task Should_Set_Name()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                var show = await _fileEntryRepository.FirstOrDefaultAsync();
                show.Filename = "FBI";
                await _fileEntryRepository.UpdateAsync(show, true);
            });

            var  dbShowList = await _fileEntryRepository.GetListAsync();
            var dbShow = dbShowList[0];
            dbShow.Filename.ShouldNotBeNull();
        }
    }
}
