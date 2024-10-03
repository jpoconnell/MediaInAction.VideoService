using System.Text;
using Newtonsoft.Json;

namespace MediaInAction.VideoService.Functional.Test.Helper
{
    internal class HttpHelper
    {
        public static StringContent GetJsonHttpContent(object items)
        {
            return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
        }

        internal static class Urls
        {
            public readonly static string GetAllSeries = "/api/Series/GetAllAsync";
            public readonly static string GetSeries = "/api/Series/GetAsync";
            public readonly static string AddSeries = "/api/Series/AddAsync";
            public readonly static string EditSeries = "/api/Series/UpdateAsync";
            public readonly static string DeleteSeries = "/api/Series/DeleteAsync";
        }
    }
}
