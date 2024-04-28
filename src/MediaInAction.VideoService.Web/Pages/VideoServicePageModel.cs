using MediaInAction.VideoService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MediaInAction.VideoService.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class VideoServicePageModel : AbpPageModel
{
    protected VideoServicePageModel()
    {
        LocalizationResourceType = typeof(VideoServiceResource);
    }
}
