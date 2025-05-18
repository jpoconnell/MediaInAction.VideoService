using MediaInAction.FileService.FileEntriesNs;
using MongoDB.Bson;
using MongoDB.Driver;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace MediaInAction.FileService.MongoDb
{
    public static class FileServiceMongoDbContextExtensions
    {
        public static void ConfigureFileService(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<FileEntry>(x =>
            {
                x.CollectionName = "FileEntries";
                /*
                x.ConfigureIndexes(indexes =>
                    {
                        indexes.CreateOne(
                            new CreateIndexModel<BsonDocument>(
                                Builders<BsonDocument>.IndexKeys.Ascending("Server,Filename,Directory,Extn,ListName"),
                                new CreateIndexOptions { Unique = true }
                            )
                        );
                    }
                );
                */
            });
        }
    }
}
//db.members.createIndex( { groupNumber: 1, lastname: 1, firstname: 1 }, { unique: true } )