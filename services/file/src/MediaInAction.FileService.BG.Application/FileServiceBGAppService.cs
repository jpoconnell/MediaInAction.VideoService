using MediaInAction.FileService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.FileService.BG
{
    public abstract class FileServiceBGAppService : ApplicationService
    {
        protected FileServiceBGAppService()
        {
            LocalizationResource = typeof(FileServiceResource);
            ObjectMapperContext = typeof(FileServiceBGApplicationModule);
        }
    }
}
