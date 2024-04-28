using MediaInAction.VideoService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.VideoService.Permissions;

public class VideoServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(VideoServicePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(VideoServicePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<VideoServiceResource>(name);
    }
}
