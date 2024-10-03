namespace MediaInAction.VideoService;

public static class VideoServiceDomainErrorCodes
{
    public static string? EpisodeAlreadyExists = "VideoService:00001";
    public static string? EpisodeDoesNotExistException = "VideoService:00002";
    public static string? MovieAlreadyExists = "VideoService:00003";
    public static string? SeriesWithIdNotFound = "VideoService:00004";
}
