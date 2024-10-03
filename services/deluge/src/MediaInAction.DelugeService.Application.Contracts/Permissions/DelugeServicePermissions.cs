using Volo.Abp.Reflection;

namespace MediaInAction.DelugeService.Permissions
{
    public static class DelugeServicePermissions
    {
        public const string GroupName = "DelugeService";

        public static class Products
        {
            public const string Default = GroupName + ".Products";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(DelugeServicePermissions));
        }
    }
}