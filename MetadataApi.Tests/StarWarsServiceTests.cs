using System.Net;
using Moq.Protected;
using Newtonsoft.Json.Linq;

namespace MetadataApi.Tests;

public class StarWarsServiceTests
{
    private readonly Mock<ILogger<StarWarsService>> _mockLogger;
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly HttpClient _client;
    private readonly StarWarsService _service;

    public StarWarsServiceTests()
    {
        // Mock ILogger
        _mockLogger = new Mock<ILogger<StarWarsService>>();

        // Setup HttpClient with a Mock Handler
        _handlerMock = new Mock<HttpMessageHandler>();
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((req, _) => Console.WriteLine(req.RequestUri))
            .ReturnsAsync((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                if (request.RequestUri.AbsolutePath.EndsWith("api/people/1"))
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(TestData.GetPeopleOne()) };
                else if (request.RequestUri.AbsolutePath.EndsWith("api/planets/1"))
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(TestData.GetPlanetOne()) };
                else if (request.RequestUri.AbsolutePath.EndsWith("api/planets/1/"))
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(TestData.GetPlanetOne()) };
                else if (request.RequestUri.AbsolutePath.EndsWith("api/vehicles/14/"))
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(TestData.GetVehicles14()) };
                else if (request.RequestUri.AbsolutePath.EndsWith("api/vehicles/30/"))
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(TestData.GetVehicles30()) };
                else if (request.RequestUri.AbsolutePath.EndsWith("api/"))
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(TestData.GetAvailableTypes()) };
                else
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
            });

        _client = new HttpClient(_handlerMock.Object)
        {
            BaseAddress = new Uri("http://example.com")  // Use the base address if necessary
        };

        // Initialize the service with the mocked logger and HttpClient
        _service = new StarWarsService(_mockLogger.Object, _client);
    }

    [Fact]
    public async Task GetSingle_People_ShouldReturn_PersonDetails()
    {
        // Arrange - in setup

        // Act
        var actual = await _service.GetSingleRequestAsync("people", 1);
        var expected = JObject.Parse(TestData.GetPeopleOne());

        // Assert
        _handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.AbsolutePath.EndsWith("api/people/1")),
            ItExpr.IsAny<CancellationToken>()
        );

        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetSingle_Planet_ShouldReturn_PlanetDetails()
    {
        // Arrange - in setup

        // Act
        var actual = await _service.GetSingleRequestAsync("planets", 1);
        var expected = JObject.Parse(TestData.GetPlanetOne());

        // Assert
        _handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.AbsolutePath.EndsWith("api/planets/1")),
            ItExpr.IsAny<CancellationToken>()
        );

        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetSingle_WrongPeople_ShouldReturn_HttpRequestException()
    {
        // Arrange - in setup

        // Act
        Func<Task> act = () => _service.GetSingleRequestAsync("people", 1001);

        //Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(act);
    }

    [Fact]
    public async Task GetSingle_GetInvalidType_ShouldReturn_HttpRequestException()
    {
        // Arrange - in setup

        // Act
        Func<Task> act = () => _service.GetSingleRequestAsync("invalid", 1);

        //Assert
        await Assert.ThrowsAsync<HttpRequestException>(act);
    }

    [Fact]
    public async Task GetAvailableType_ShouldReturn_List()
    {
        // Arrange - in setup

        // Act
        var actual = await _service.GetAvailableTypesAsync();
        var json = JObject.Parse(TestData.GetAvailableTypes());
        var expected = json.Properties().Select(p => p.Name);

        // Assert
        _handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.AbsolutePath.EndsWith("api/")),
            ItExpr.IsAny<CancellationToken>()
        );

        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetHydrated_PeopleOne_ShouldReturn_PersonWithSingle()
    {
        // Arrange - in setup

        // Act
        var actual = await _service.GetHydratedRequestAsync("people", 1, new() { "homeworld" });
        var expected = JObject.Parse(TestData.GetPeopleOneHydratedWithPlanetOne());

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetHydrated_PeopleOne_Invalid_ShouldReturn_PersonDetails()
    {
        // Arrange - in setup

        // Act
        var actual = await _service.GetHydratedRequestAsync("people", 1, new() { "invalid" });
        var expected = JObject.Parse(TestData.GetPeopleOne());

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }


    [Fact]
    public async Task GetHydrated_PeopleOne_ShouldReturn_PersonWithList()
    {
        // Arrange - in setup

        // Act
        var actual = await _service.GetHydratedRequestAsync("people", 1, new() { "vehicles" });
        var expected = JObject.Parse(TestData.GetPeopleOneHydratedWithVehicles());

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

}
