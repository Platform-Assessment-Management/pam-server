using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAMApplication;
using PAMApplication.PlatformContracts;

namespace PAMCore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private PlatformApplication PlatformApp;

        public PlatformController(PlatformApplication platformApp) => this.PlatformApp = platformApp;

        [HttpPost]
        public async Task CreateProject(PlatformCreateRequest platformRequest)
        {
            await PlatformApp.CreatePlatform(platformRequest);
        }
    }
}
