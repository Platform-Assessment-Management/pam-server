using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAMApplication;
using PAMApplication.MaturityModelContracts;

namespace PAMCore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MaturyModelController : ControllerBase
    {
        private MaturityModelApplication MatirityApp;

        public MaturyModelController(MaturityModelApplication matirityApp) => this.MatirityApp = matirityApp;

        [HttpGet]
        public async Task<IList<MaturityModelListResponse>> List()
        {
            return await MatirityApp.ListMaturityModels();
        }
    }
}
