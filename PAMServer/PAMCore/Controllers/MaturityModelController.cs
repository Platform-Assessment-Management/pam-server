using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAMApplication;
using PAMApplication.CampContracts;
using PAMApplication.MaturityModelContracts;

namespace PAMCore.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class MaturityModelController : ControllerBase
    {
        private MaturityModelApplication maturityApp;

        public MaturityModelController(MaturityModelApplication matirityApp) => this.maturityApp = matirityApp;

        [HttpGet("maturitymodel")]
        public async Task<IList<MaturityModelListResponse>> List()
        {
            return await maturityApp.ListMaturityModels();
        }

        [HttpPost("maturitymodel")]
        public async Task<MaturityModelCreateResponse> CreateMaturityModel(MaturityModelCreateRequest request)
        {
            return await maturityApp.CreateMaturity(request);
        }

        [HttpPost("maturitymodel/{maturityId}/options")]
        public async Task IncludeOptions([FromRoute]Guid maturityId, MaturityModelIncludeOptionRequest request)
        {
            request.MaturityId = maturityId;
            await maturityApp.IncludeOption(request);
        }

        [HttpPut("maturitymodel/{maturityId}/options/{value}")]
        public async Task IncludeOptions([FromRoute] Guid maturityId, [FromRoute] int value, MaturityModelAlterOptionRequest request)
        {
            request.MaturityId = maturityId;
            request.value = value;
            await maturityApp.AlterOptionAsync(request);
        }

        [HttpGet("camp/project/{projectId}")]
        public async Task<MaturityModelAssessmentResponse> GetValidation(Guid projectId)
        {
            return await maturityApp.Assesment(projectId);
        }

        [HttpPost("camp")]
        public async Task<CampCreateResponse> CreateCamp(CampCreateRequest request)
        {
            return await maturityApp.CreateCampAsync(request);
        }

        [HttpPut("camp")]
        public async Task CreateCamp(CampAlterRequest request)
        {
            await this.maturityApp.AlterCampAsync(request);
        }

        [HttpGet("camp")]
        public async Task<CampListResponse> getCamp()
        {
            return await this.maturityApp.ListCampAsync();

        }

        [HttpPost("camp/{campId}/maturitymodel/{maturityModelId}/level/{level}")]
        public async Task DefineMaturityModelCamp([FromRoute] Guid campId, [FromRoute] Guid maturityModelId, [FromRoute] int level)
        {
            await maturityApp.DefineMaturityModelCampAsync(new MaturityModelCampDefineRequest
            {
                CampId = campId,
                MaturityModelId = maturityModelId,
                Level = level
            });
        }

        [HttpDelete("camp/{campId}/maturitymodel/{maturitymodel}")]
        public Task RemoveMaturityModelCamp()
        {
            throw new NotImplementedException();
        }
    }
}
