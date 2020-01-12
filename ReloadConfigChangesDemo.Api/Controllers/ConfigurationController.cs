using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReloadConfigChangesDemo.Api.Services;

namespace ReloadConfigChangesDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet]
        [Route("values")]
        public ActionResult<IEnumerable<string>> GetValues()
        {
            return _configurationService.GetValues();
        }

        [HttpGet("{key}")]
        public ActionResult<string> GetKeyValue(string key)
        {
            return _configurationService.GetKeyValue(key);
        }
    }
}
