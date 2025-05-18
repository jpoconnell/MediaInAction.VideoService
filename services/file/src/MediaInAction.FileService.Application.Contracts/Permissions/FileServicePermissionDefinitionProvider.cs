using MediaInAction.FileService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.FileService.Permissions
{
    public class FileServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var fileManagmentGroup = context.AddGroup(FileServicePermissions.GroupName, L("Permission:FileService"));

            var files = fileManagmentGroup.AddPermission(FileServicePermissions.FileEntry.Default, L("Permission:FileEntries"));
            
            fileManagmentGroup.AddPermission(FileServicePermissions.FileEntry.Dashboard, L("Permission:Dashboard"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FileServiceResource>(name);
        }
    }
}