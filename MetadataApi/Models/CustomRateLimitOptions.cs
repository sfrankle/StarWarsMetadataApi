using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetadataApi.Models;

public class CustomRateLimitOptions
{
    public const string ConfigSection = "RateLimiting";
    public int PermitLimit { get; set; }
    public int WindowInSeconds { get; set; }

}
