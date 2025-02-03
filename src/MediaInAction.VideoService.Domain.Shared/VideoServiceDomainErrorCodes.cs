namespace MediaInAction.VideoService;

public static class VideoServiceDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    public static string MovieAlreadyExists = "VideoService:001";
    public static string EpisodeAlreadyExists = "VideoService:002";
    public static string EpisodeDoesNotExistException = "VideoService:003";
    public static string SeriesWithIdNotFound = "VideoService:004";
}
