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

    // todo: add rate limiting
    [HttpGet(Name = "GET Star Wars Types")]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _starWarsService.GetAvailableTypesAsync());
    }

    [HttpGet("{type}/{id}", Name = "GET Star Wars Object")]
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


    [HttpGet("nested/{type}/{id}", Name = "GET complex Star Wars Object")]
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
            return StatusCode(500, defaultProblemDetails);
        }
    }
}
