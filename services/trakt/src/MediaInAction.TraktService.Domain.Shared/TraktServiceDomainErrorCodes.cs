namespace MediaInAction.TraktService
{
    public static class TraktServiceDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */
        public static string SeriesNotFound { get; set; }
        public static string EpisodeIdNotFound { get; set; }
        public static string EpisodeNotFound { get; set; }
        public static string MovieNotFound { get; set; }
    }
}
