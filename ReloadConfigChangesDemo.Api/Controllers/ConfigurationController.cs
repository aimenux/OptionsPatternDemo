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
        [Route("keys")]
        public ActionResult<IEnumerable<string>> GetKeys()
        {
            return _configurationService.GetAllKeys();
        }

        [HttpGet]
        [Route("values")]
        public ActionResult<IEnumerable<string>> GetValues()
        {
            return _configurationService.GetAllValues();
        }

        [HttpGet("{key}")]
        public ActionResult<string> Get(string key)
        {
            return _configurationService.GetKeyValue(key);
        }
    }
}
