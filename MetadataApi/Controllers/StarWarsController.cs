using MetadataApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetadataApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class StarWarsController : Controller
{
    private readonly ILogger<StarWarsController> _logger;
    private readonly IStarWarsService _starWarsService;
    private static readonly string defaultErrorMessage = "An error occurred while processing your request.";
    private static ProblemDetails defaultProblemDetails = new ProblemDetails
    {
        Status = StatusCodes.Status500InternalServerError,
        Title = defaultErrorMessage,
        Detail = defaultErrorMessage
    };

    public StarWarsController(ILogger<StarWarsController> logger, IStarWarsService starWarsService)
    {
        _logger = logger;
        _starWarsService = starWarsService;
    }

    [HttpGet(Name = "GET StarWars Types")]
    public async Task<IActionResult> GetTypesAsync()
    {
        try
        {
            return Ok(await _starWarsService.GetAvailableTypesAsync());
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, defaultProblemDetails);
        }

    }

    [HttpGet("{type}/{id}", Name = "GET one StarWars Object")]
    public async Task<IActionResult> GetAsync(string type, int id)
    {
        try
        {
            var response = await _starWarsService.GetSingleRequestAsync(type, id);
            _logger.LogInformation(response.ToString());
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, defaultProblemDetails);
        }
    }

    [HttpGet("hydrated/{type}/{id}", Name = "GET one Star Wars Object hydrated")]
    public async Task<IActionResult> GetAsync(string type, int id, [FromQuery] IEnumerable<string> properties)
    {
        try
        {
            var response = await _starWarsService.GetHydratedRequestAsync(type, id, properties.ToHashSet());
            _logger.LogInformation(response.ToString());
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError, defaultProblemDetails);
        }
    }
}
