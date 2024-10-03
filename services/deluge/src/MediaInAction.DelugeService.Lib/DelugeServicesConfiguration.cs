using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService.Bg
{
    public class DelugeServicesConfiguration
    {
        public string ServerUrl { get; set; }
        public string DelugeUsername { get; set; }
        public string Password { get; set; }

        
        public DelugeServicesConfiguration(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            ServerUrl = configuration["Deluge:Url"];
            DelugeUsername = configuration["Deluge:Username"];
            Password = configuration["Deluge:Password"];
        }
    }
}
