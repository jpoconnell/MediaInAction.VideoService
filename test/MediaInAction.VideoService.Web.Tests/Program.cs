using Microsoft.AspNetCore.Builder;
using MediaInAction.VideoService;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<VideoServiceWebTestModule>();

public partial class Program
{
}
