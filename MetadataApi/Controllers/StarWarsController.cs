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

    public StarWarsController(ILogger<StarWarsController> logger, IStarWarsService starWarsService)
    {
        _logger = logger;
        _starWarsService = starWarsService;
    }

    [HttpGet(Name = "GetStarWarsInfo")]
    public async Task<IActionResult> GetAsync()
    {
        var j = await _starWarsService.GetSingleRequestAsync("people/1");
        _logger.LogInformation(j.ToString());
        return Ok(j);
    }
}
