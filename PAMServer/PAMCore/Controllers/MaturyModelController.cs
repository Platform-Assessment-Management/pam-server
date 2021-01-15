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
        private MaturityModelApplication maturityApp;

        public MaturyModelController(MaturityModelApplication matirityApp) => this.maturityApp = matirityApp;

        [HttpGet]
        public async Task<IList<MaturityModelListResponse>> List()
        {
            return await maturityApp.ListMaturityModels();
        }

        [HttpPost]
        public async Task<MaturityModelCreateResponse> CreateMaturityModel(MaturityModelCreateRequest request)
        {
            return await maturityApp.CreateMaturity(request);
        }

        [HttpPost("{maturityId}/options")]
        public async Task IncludeOptions([FromRoute]Guid maturityId, MaturityModelIncludeOptionRequest request)
        {
            request.MaturityId = maturityId;
            await maturityApp.IncludeOption(request);
        }

        [HttpPut("{maturityId}/options/{value}")]
        public async Task IncludeOptions([FromRoute] Guid maturityId, [FromRoute] int value, MaturityModelAlterOptionRequest request)
        {
            request.MaturityId = maturityId;
            request.value = value;
            await maturityApp.AlterOptionAsync(request);
        }

        [HttpPost("{maturityId}/chapters")]
        public async Task DefineChapters([FromRoute] Guid maturityId, MaturityModelDefineChaptersRequest request)
        {
            request.MaturityId = maturityId;
            await maturityApp.DefineChaptersAsync(request);
        }

        [HttpGet("project/{projectId}/camp")]
        public async Task GetValidation(Guid projectId)
        {
            await maturityApp.Assesment(projectId);
        }
    }
}
