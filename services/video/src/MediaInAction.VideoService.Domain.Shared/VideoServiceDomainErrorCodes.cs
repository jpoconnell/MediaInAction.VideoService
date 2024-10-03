namespace MediaInAction.VideoService;

public static class VideoServiceDomainErrorCodes
{
    public static string EpisodeAlreadyExists { get; set; }
    public static string EpisodeDoesNotExistException { get; set; }
    public static string MovieAlreadyExists { get; set; }
    public static string SeriesWithIdNotFound { get; set; }
}