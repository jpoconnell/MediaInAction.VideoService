using MediaInAction.AdministrationService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.AdministrationService
{
    /* Inherit your application services from this class.
     */
    public abstract class AdministrationServiceAppService : ApplicationService
    {
        protected AdministrationServiceAppService()
        {
            LocalizationResource = typeof(AdministrationServiceResource);
        }
    }
}
