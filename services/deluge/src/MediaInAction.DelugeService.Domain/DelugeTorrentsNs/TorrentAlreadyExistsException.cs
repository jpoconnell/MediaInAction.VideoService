using Volo.Abp;

namespace MediaInAction.DelugeService.DelugeTorrentsNs
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