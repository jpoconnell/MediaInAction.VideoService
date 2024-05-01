﻿using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Repositories;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class ToBeMappedRepositoryTests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly IRepository<ToBeMapped, Guid> _toBeMappedRepository;

    public ToBeMappedRepositoryTests()
    {
        _toBeMappedRepository = GetRequiredService<IRepository<ToBeMapped, Guid>>();
    }

    [Fact]
    public async Task Should_Query_AppUser()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var toBeMapped = await (await _toBeMappedRepository.GetQueryableAsync())
               // .Where(u => u.UserName == "admin")
                .FirstOrDefaultAsync();

                //Assert
                toBeMapped.ShouldNotBeNull();
        });
    }
    
    //
}
