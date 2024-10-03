using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService
{
    public class FileServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly FileEntryManager _fileEntryManager;
        private readonly TestData _testData;

        public FileServiceTestDataSeedContributor(
            FileEntryManager fileEntryManager,
          TestData   testData)
        {
            _fileEntryManager = fileEntryManager;
            _testData = testData;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedTestFileServicesAsync();
        }

        private async Task SeedTestFileServicesAsync()
        {
            var fileEntry1 = new FileEntryCreateDto();
            fileEntry1.Server = _testData.Server;
            fileEntry1.FileName = _testData.Filename1;
            fileEntry1.Directory = _testData.Directory1;
            fileEntry1.ListName = _testData.List1;
            fileEntry1.Extn = _testData.Extn1;
            await _fileEntryManager.CreateAsync(fileEntry1);
            
            var fileEntry2 = new FileEntryCreateDto();
            fileEntry2.Server = _testData.Server;
            fileEntry2.FileName = _testData.Filename2;
            fileEntry2.Directory = _testData.Directory2;
            fileEntry2.ListName = _testData.List1;
            fileEntry2.Extn = _testData.Extn1;
            await _fileEntryManager.CreateAsync(fileEntry2);
        }
    }
}