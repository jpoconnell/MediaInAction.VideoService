namespace MediaInAction.VideoService.Functional.Test.Abstractions;

public class BaseFunctionalTest : IClassFixture<FunctionalWebApplicationFactory>
{
    protected readonly FunctionalWebApplicationFactory _factory;
    protected HttpClient HttpClient {get; init;}
    
    public BaseFunctionalTest(FunctionalWebApplicationFactory factory)
    {
        HttpClient = factory.CreateClient();
    }
    
}