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
            var products = productManagementGroup.AddPermission(DelugeServicePermissions.Products.Default, L("Permission:Products"));
            products.AddChild(DelugeServicePermissions.Products.Update, L("Permission:Products.Edit"));
            products.AddChild(DelugeServicePermissions.Products.Delete, L("Permission:Products.Delete"));
            products.AddChild(DelugeServicePermissions.Products.Create, L("Permission:Products.Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DelugeServiceResource>(name);
        }
    }
}
