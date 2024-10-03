using System.Threading.Tasks;
using DelugeRPCClient.Net;
using Shouldly;

namespace MediaInAction.DelugeService;

public class ServiceTests
{
    [Fact]
    public async Task Should_Login()
    {
        var client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

        var loginResult = await client.Login();
        loginResult.ShouldBe(true);
    }
    
    /*
    [Fact]
    public async Task Should_Get_Labels()
    {
        var client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

        var loginResult = await client.Login();
        loginResult.ShouldBe(true);
   
        var labels = await client.ListLabels();
        labels.ShouldNotBeNull();

        var logoutResult = await client.Logout();
        logoutResult.ShouldBe(true);
    }
    
    */
    [Fact]
    public async Task Should_Get_Torrents()
    {
        var client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

        var loginResult = await client.Login();
        loginResult.ShouldBe(true);
   
        var torrentList = await client.ListTorrents();
        torrentList.ShouldNotBeNull();

        var logoutResult = await client.Logout();
        
        logoutResult.ShouldBe(true);
    }
}
