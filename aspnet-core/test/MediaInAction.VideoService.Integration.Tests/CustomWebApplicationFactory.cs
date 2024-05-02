using MediaInAction.VideoService.SeriesNs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MediaInAction.VideoService.Integration.Tests
{
    public class CustomWebApplicationFactory: WebApplicationFactory<Program>
    {
        public Mock<ISeriesRepository> SeriesRepositoryMock { get; }

        public CustomWebApplicationFactory()
        {
            SeriesRepositoryMock = new Mock<ISeriesRepository>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(ReviewRepositoryMock.Object);
            });
        }
    }
}
