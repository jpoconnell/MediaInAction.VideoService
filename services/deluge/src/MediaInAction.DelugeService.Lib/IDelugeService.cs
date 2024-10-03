using DelugeRPCClient.Net;

namespace MediaInAction.DelugeService.Bg
{
    public interface IDelugeService
    {
        DelugeClient GetClient();
    }
}
