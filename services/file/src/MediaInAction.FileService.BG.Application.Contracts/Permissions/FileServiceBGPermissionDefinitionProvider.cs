using MediaInAction.FileService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.FileService.BG.Permissions
{
    public class FileServiceBGPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var fileManagmentGroup = context.AddGroup(FileServiceBGPermissions.GroupName, L("Permission:FileService"));

            var files = fileManagmentGroup.AddPermission(FileServiceBGPermissions.FileEntry.Default, L("Permission:FileEntries"));
            fileManagmentGroup.AddPermission(FileServiceBGPermissions.FileEntry.Dashboard, L("Permission:Dashboard"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FileServiceResource>(name);
        }
    }
}