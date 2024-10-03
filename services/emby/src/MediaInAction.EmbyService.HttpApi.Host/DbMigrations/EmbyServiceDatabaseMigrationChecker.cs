using MediaInAction.EmbyService.MongoDB;
using System;
using MediaInAction.Shared.Hosting.Microservices.DbMigrations.MongoDb;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace MediaInAction.EmbyService.DbMigrations;

public class EmbyServiceDatabaseMigrationChecker : PendingMongoDbMigrationsChecker<EmbyServiceMongoDbContext>
{
    public EmbyServiceDatabaseMigrationChecker(
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
            EmbyServiceDbProperties.ConnectionStringName)
    {
    }
}