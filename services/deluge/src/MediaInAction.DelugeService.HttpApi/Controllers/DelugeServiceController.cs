using MediaInAction.DelugeService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.DelugeService.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class DelugeServiceController : AbpControllerBase
    {
        protected DelugeServiceController()
        {
            LocalizationResource = typeof(DelugeServiceResource);
        }
    }
}