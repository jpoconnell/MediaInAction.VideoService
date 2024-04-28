using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.Pages;

public class Index_Tests : VideoServiceWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
