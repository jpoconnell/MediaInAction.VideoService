using MediaInAction.TraktService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.TraktService.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class FileServiceController : AbpControllerBase
    {
        protected FileServiceController()
        {
            LocalizationResource = typeof(TraktServiceResource);
        }
    }
}