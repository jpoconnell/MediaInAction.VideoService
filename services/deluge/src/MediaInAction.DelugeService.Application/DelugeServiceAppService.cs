using MediaInAction.DelugeService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.DelugeService
{
    /* Inherit your application services from this class.
     */
    public abstract class DelugeServiceAppService : ApplicationService
    {
        protected DelugeServiceAppService()
        {
            LocalizationResource = typeof(DelugeServiceResource);
        }
    }
}
