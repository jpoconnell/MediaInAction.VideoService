using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.FileService.FileMappingNs;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.FileService.MongoDb
{
    [ConnectionStringName(FileServiceDbProperties.ConnectionStringName)]
    public class FileServiceMongoDbContext : AbpMongoDbContext, IFileServiceMongoDbContext
    {
        public IMongoCollection<FileEntry> FileEntries => Collection<FileEntry>();
        public IMongoCollection<FileMapping> FileMappings => Collection<FileMapping>();
        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureFileService();
        }
    }
}