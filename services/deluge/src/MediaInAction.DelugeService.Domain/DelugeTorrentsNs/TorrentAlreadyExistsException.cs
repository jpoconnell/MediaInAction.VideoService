using Volo.Abp;

namespace MediaInAction.DelugeService.TorrentsNs
{
    public class TorrentAlreadyExistsException : BusinessException
    {
        public TorrentAlreadyExistsException(string productCode)
            : base("DelugeService:000001", $"A torrent with code {productCode} has already exists!")
        {
            WithData("productCode", productCode);
        }
    }
}