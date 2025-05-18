using MediaInAction.DelugeService.MongoDB;
using System;
using MediaInAction.Shared.Hosting.Microservices.DbMigrations.MongoDb;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace MediaInAction.DelugeService.DbMigrations;

public class DelugeServiceDatabaseMigrationChecker : PendingMongoDbMigrationsChecker<DelugeServiceMongoDbContext>
{
    public DelugeServiceDatabaseMigrationChecker(
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
            DelugeServiceDbProperties.ConnectionStringName)
    {
    }
}