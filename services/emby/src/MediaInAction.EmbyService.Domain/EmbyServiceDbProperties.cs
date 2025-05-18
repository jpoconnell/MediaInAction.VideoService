namespace MediaInAction.EmbyService
{
    public static class EmbyServiceDbProperties
    {
        public static string DbTablePrefix { get; set; } = "";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EmbyService";
    }
}
