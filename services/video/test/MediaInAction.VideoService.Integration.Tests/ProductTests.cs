namespace MediaInAction.VideoService.Integration.Test;

public class SeriesTests : BaseIntegrationTest
{
    public SeriesTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldCreateSeries()
    {
        // Arrange
        var command = new CreateSeries.Command
        {
            Name = "Test",
            Category = "Test category",
            Price = 100.0m
        };

        // Act
        var productId = await Sender.Send(command);

        // Assert
        var product = DbContext.Seriess.FirstOrDefault(p => p.Id == productId);

        Assert.NotNull(product);
    }

    [Fact]
    public async Task Get_ShouldReturnSeries_WhenSeriesExists()
    {
        // Arrange
        var productId = await CreateSeries();
        var query = new GetSeries.Query { Id = productId };

        // Act
        var productResponse = await Sender.Send(query);

        // Assert
        Assert.NotNull(productResponse);
    }

    [Fact]
    public async Task Get_ShouldThrow_WhenSeriesIsNull()
    {
        // Arrange
        var query = new GetSeries.Query { Id = Guid.NewGuid() };

        // Act
        Task Action() => Sender.Send(query);

        // Assert
        await Assert.ThrowsAsync<ApplicationException>(Action);
    }

    [Fact]
    public async Task Update_ShouldUpdateSeries_WhenSeriesExists()
    {
        // Arrange
        var productId = await CreateSeries();
        var command = new UpdateSeries.Command
        {
            Id = productId,
            Name = "Test",
            Category = "Test category",
            Price = 100.0m
        };

        // Act
        await Sender.Send(command);

        // Assert
    }

    [Fact]
    public async Task Update_ShouldThrow_WhenSeriesIsNull()
    {
        // Arrange
        var command = new UpdateSeries.Command
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Category = "Test category",
            Price = 100.0m
        };

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ApplicationException>(Action);
    }

    [Fact]
    public async Task Delete_ShouldDeleteSeries_WhenSeriesExists()
    {
        // Arrange
        var productId = await CreateSeries();
        var command = new DeleteSeries.Command { Id = productId };

        // Act
        await Sender.Send(command);

        // Assert
        var product = DbContext.Seriess.FirstOrDefault(p => p.Id == productId);

        Assert.Null(product);
    }

    [Fact]
    public async Task Delete_ShouldThrow_WhenSeriesIsNull()
    {
        // Arrange
        var command = new DeleteSeries.Command { Id = Guid.NewGuid() };

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ApplicationException>(Action);
    }

    private async Task<Guid> CreateSeries()
    {
        var command = new CreateSeries.Command
        {
            Name = "Test",
            Category = "Test category",
            Price = 100.0m
        };

        var productId = await Sender.Send(command);

        return productId;
    }
}