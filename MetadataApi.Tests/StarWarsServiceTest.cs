using System.Net;
using Moq.Protected;
using Newtonsoft.Json.Linq;

namespace MetadataApi.Tests;

public class StarWarsServiceTest
{
    private readonly Mock<ILogger<StarWarsService>> _mockLogger;
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly HttpClient _client;
    private readonly StarWarsService _service;

    public StarWarsServiceTest()
    {
        // Mock ILogger
        _mockLogger = new Mock<ILogger<StarWarsService>>();

        // Setup HttpClient with a Mock Handler
        _handlerMock = new Mock<HttpMessageHandler>();
        _handlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                   "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.AbsolutePath.EndsWith("/people/1")),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(TestData.GetStarWarsCharacter())
               });

        _client = new HttpClient(_handlerMock.Object)
        {
            BaseAddress = new Uri("http://example.com")  // Use the base address if necessary
        };

        // Initialize the service with the mocked logger and HttpClient
        _service = new StarWarsService(_mockLogger.Object, _client);
    }

    [Fact]
    public async Task GetSingle_GetPeople_ShouldReturn_CharacterDetails()
    {
        // Arrange - in setup

        // Act
        var actual = await _service.GetSingleRequestAsync("people", 1);
        var expected = JObject.Parse(TestData.GetStarWarsCharacter());

        // Assert
        _handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(), // Ensure that the SendAsync method was called exactly once
            ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.AbsolutePath.EndsWith("/people/1")),
            ItExpr.IsAny<CancellationToken>()
        );

        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }
}
