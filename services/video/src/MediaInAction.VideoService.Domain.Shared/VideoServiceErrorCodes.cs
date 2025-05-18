namespace MediaInAction.VideoService;

public static class VideoServiceErrorCodes
{
    public const string OrderingStatusNotFound = "Ordering:00001";
    public const string InvalidUnits = "Ordering:00002";
    public const string InvalidDiscount = "Ordering:00003";
    public const string InvalidTotalForDiscount = "Ordering:00004";
    public const string OrderIdIdNotGuid = "Ordering:01000";
    public const string OrderWithIdNotFound = "Ordering:01001";
    public static string MovieAlreadyExists { get; set; }
    public static string EpisodeAlreadyExists { get; set; }
    public static string EpisodeDoesNotExistException { get; set; }
    public static string SeriesWithIdNotFound { get; set; }
}