using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetadataApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MetadataApi.Tests;

public class StarWarsControllerTests
{
    private readonly StarWarsController _controller;
    private readonly Mock<IStarWarsService> _mockService;

    public StarWarsControllerTests()
    {
        var logger = new Mock<ILogger<StarWarsController>>();
        _mockService = new Mock<IStarWarsService>();

        _controller = new StarWarsController(logger.Object, _mockService.Object);
    }

    [Fact]
    public async Task GetTypes_ReturnsOk_WithDataAsync()
    {
        // Arrange
        var expected = TestData.GetAvailableTypeList();
        _mockService.Setup(s => s.GetAvailableTypesAsync()).ReturnsAsync(expected);

        // Act
        var response = await _controller.GetTypesAsync();

        // Assert
        var result = Assert.IsType<OkObjectResult>(response);
        var actual = Assert.IsAssignableFrom<IEnumerable<string>>(result.Value);

        Assert.Equal(expected.Count(), actual.Count());

        foreach (var type in expected)
        {
            Assert.Contains(type, actual);
        }
    }

    [Fact]
    public async Task GetSingle_ReturnsOk_WithDataAsync()
    {
        // Arrange
        var expected = JObject.Parse(TestData.GetPeopleOne());
        _mockService.Setup(s => s.GetSingleRequestAsync("people", 1)).ReturnsAsync(expected);

        // Act
        var response = await _controller.GetAsync("people", 1);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response);
        var actual = Assert.IsType<JObject>(result.Value);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetSingle_InvalidType_Returns_Exception()
    {
        // Arrange
        _mockService.Setup(s => s.GetSingleRequestAsync("invalid", 1)).Throws(new HttpRequestException());

        // Act
        var response = await _controller.GetAsync("invalid", 1);

        // Assert
        var result = Assert.IsType<ObjectResult>(response);
        Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
    }

    [Fact]
    public async Task GetHydrated_ReturnsOk_WithDataAsync()
    {
        // Arrange
        var expected = JObject.Parse(TestData.GetPeopleOneHydratedWithPlanetOne());
        _mockService.Setup(s => s.GetHydratedRequestAsync("people", 1, new() { "homeworld" })).ReturnsAsync(expected);

        // Act
        var response = await _controller.GetAsync("people", 1, ["homeworld"]);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response);
        var actual = Assert.IsType<JObject>(result.Value);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetHydratedList_ReturnsOk_WithDataAsync()
    {
        // Arrange
        var expected = JObject.Parse(TestData.GetPeopleOneHydratedWithVehicles());
        _mockService.Setup(s => s.GetHydratedRequestAsync("people", 1, new() { "vehicles" })).ReturnsAsync(expected);

        // Act
        var response = await _controller.GetAsync("people", 1, ["vehicles"]);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response);
        var actual = Assert.IsType<JObject>(result.Value);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetHydrated_InvalidAttribute_ReturnsOk_WithSingleDataAsync()
    {
        // Arrange
        var expected = JObject.Parse(TestData.GetPeopleOne());
        _mockService.Setup(s => s.GetHydratedRequestAsync("people", 1, new() { "invalid" })).ReturnsAsync(expected);

        // Act
        var response = await _controller.GetAsync("people", 1, ["invalid"]);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response);
        var actual = Assert.IsType<JObject>(result.Value);
        Assert.Equal(expected, actual);
    }
}
