// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Integration;
using Volo.Abp.Users;

// ReSharper disable once CheckNamespace
namespace Volo.Abp.Identity.Integration;

public interface IIdentityUserIntegrationService : IApplicationService
{
    Task<String[]> GetRoleNamesAsync(Guid id);

    Task<UserData> FindByIdAsync(Guid id);

    Task<UserData> FindByUserNameAsync(string userName);

    Task<ListResultDto<UserData>> SearchAsync(UserLookupSearchInputDto input);

    Task<long> GetCountAsync(UserLookupCountInputDto input);
}
