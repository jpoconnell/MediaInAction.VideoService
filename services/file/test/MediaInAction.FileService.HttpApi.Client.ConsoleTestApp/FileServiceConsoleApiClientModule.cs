using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace MediaInAction.FileService
{
    [DependsOn(
        typeof(FileServiceHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class FileServiceConsoleApiClientModule : AbpModule
    {
        
    }
}
