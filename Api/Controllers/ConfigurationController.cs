using System;
using System.Collections.Generic;
using Api.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ConfigurationController : ControllerBase
{
    private readonly Func<string, IConfigurationService> _serviceResolver;

    public ConfigurationController(Func<string, IConfigurationService> serviceResolver)
    {
        _serviceResolver = serviceResolver;
    }

    [HttpGet("entries")]
    public IEnumerable<string> GetEntries([FromQuery] string choice = nameof(Choices.ConfigurationOptionsService))
    {
        var configurationService = _serviceResolver(choice);
        return configurationService.GetEntries();
    }
}