using MediaInAction.DelugeService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.DelugeService.Permissions
{
    public class DelugeServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var productManagementGroup = context.AddGroup(DelugeServicePermissions.GroupName, L("Permission:GroupName"));
            var products = productManagementGroup.AddPermission(DelugeServicePermissions.Torrents.Default, L("Permission:Torrents"));
            products.AddChild(DelugeServicePermissions.Torrents.Update, L("Permission:Torrents.Edit"));
            products.AddChild(DelugeServicePermissions.Torrents.Delete, L("Permission:Torrents.Delete"));
            products.AddChild(DelugeServicePermissions.Torrents.Create, L("Permission:Torrents.Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DelugeServiceResource>(name);
        }
    }
}
