using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;

namespace MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp
{
    public class ConsoleTestAppHostedService : IHostedService
    {
        private readonly IConfiguration _configuration;

        public ConsoleTestAppHostedService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<EmbyServiceConsoleApiClientModule>(options =>
            {
                options.Services.ReplaceConfiguration(_configuration);
            }))
            {
                application.Initialize();

                //var demo = application.ServiceProvider.GetRequiredService<ClientDemoService>();
                //await demo.RunAsync();

                application.Shutdown();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
