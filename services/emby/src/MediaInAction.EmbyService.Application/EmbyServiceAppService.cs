using MediaInAction.EmbyService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService
{
    /* Inherit your application services from this class.
     */
    public abstract class EmbyServiceAppService : ApplicationService
    {
        protected EmbyServiceAppService()
        {
            LocalizationResource = typeof(EmbyServiceResource);
        }
    }
}
