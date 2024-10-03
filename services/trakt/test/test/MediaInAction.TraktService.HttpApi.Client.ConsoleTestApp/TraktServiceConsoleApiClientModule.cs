using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class TraktServiceConsoleApiClientModule : AbpModule
    {
        
    }
}
