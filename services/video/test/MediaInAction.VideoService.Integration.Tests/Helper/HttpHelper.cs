using System.Text;
using Newtonsoft.Json;

namespace MediaInAction.VideoService.Integration.Test.Helper
{
    internal class HttpHelper
    {
        public static StringContent GetJsonHttpContent(object items)
        {
            return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
        }

        internal static class Urls
        {
            public readonly static string GetAllSeries = "/api/Student/GetAllAsync";
            public readonly static string GetSeries = "/api/Student/GetAsync";
            public readonly static string AddSeries = "/api/Student/AddAsync";
            public readonly static string EditSeries = "/api/Student/UpdateAsync";
            public readonly static string DeleteSeries = "/api/Student/DeleteAsync";
        }
    }
}
