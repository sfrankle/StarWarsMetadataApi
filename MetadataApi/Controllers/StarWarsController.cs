using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetadataApi.Controllers;

[Route("[controller]")]
public class StarWarsController : Controller
{
    private readonly ILogger<StarWarsController> _logger;

    public StarWarsController(ILogger<StarWarsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetStarWarsInfo")]
    public string Get()
    {
        return "I work!";
    }
}
