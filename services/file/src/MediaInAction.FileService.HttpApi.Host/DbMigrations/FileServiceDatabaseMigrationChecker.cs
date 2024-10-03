using System;
using MediaInAction.FileService.MongoDb;
using MediaInAction.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace MediaInAction.FileService.DbMigrations;

public class FileServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<FileServiceDbContext>
{
    public FileServiceDatabaseMigrationChecker(
        IUnitOfWorkManager unitOfWorkManager,
        IServiceProvider serviceProvider,
        ICurrentTenant currentTenant,
        IDataSeeder dataSeeder,
        IAbpDistributedLock distributedLockProvider)
        : base(
            unitOfWorkManager,
            serviceProvider,
            currentTenant,
            dataSeeder,
            distributedLockProvider,
            FileServiceDbProperties.ConnectionStringName)
    {
    }
}