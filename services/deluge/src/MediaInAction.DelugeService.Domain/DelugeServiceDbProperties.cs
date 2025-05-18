namespace MediaInAction.DelugeService
{
    public static class DelugeServiceDbProperties
    {
        public static string DbTablePrefix { get; set; } = "";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "DelugeService";
    }
}
