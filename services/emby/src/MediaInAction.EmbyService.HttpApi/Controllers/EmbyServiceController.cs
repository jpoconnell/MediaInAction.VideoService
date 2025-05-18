using MediaInAction.EmbyService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.EmbyService.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class EmbyServiceController : AbpControllerBase
    {
        protected EmbyServiceController()
        {
            LocalizationResource = typeof(EmbyServiceResource);
        }
    }
}