using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.FileService.MongoDb;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace MediaInAction.FileService.FileMappingNs;

public class MongoDbFileMappingRepository
    : MongoDbRepository<FileServiceMongoDbContext, FileMapping, Guid>,
    IFileMappingRepository
{
    public MongoDbFileMappingRepository(
        IMongoDbContextProvider<FileServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public async Task<List<FileMapping>> GetUnMapped()
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
      
            .Where(file => file.IsSent == false)
            .As<IMongoQueryable<FileMapping>>()
            .ToListAsync();
    }
}
