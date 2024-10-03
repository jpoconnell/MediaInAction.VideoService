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
            var fileEntry1 = new FileEntryCreateDto
            {
                Server = _testData.Server,
                FileName = _testData.Filename1,
                Directory = _testData.Directory1,
                ListName = _testData.List1,
                Extn = _testData.Extn1
            };
            await _fileEntryManager.CreateAsync(fileEntry1);
            
            var fileEntry2 = new FileEntryCreateDto
            {
                Server = _testData.Server,
                FileName = _testData.Filename2,
                Directory = _testData.Directory2,
                ListName = _testData.List1,
                Extn = _testData.Extn1
            };
            await _fileEntryManager.CreateAsync(fileEntry2);
        }
    }
}