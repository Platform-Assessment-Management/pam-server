using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAMApplication;
using PAMApplication.ProjectContracts;

namespace PAMCore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ProjectApplication ProjectApp;

        public ProjectController(ProjectApplication projectApp) => this.ProjectApp = projectApp;

        [HttpPost]
        public async Task CreateProject(ProjectCreateRequest projectRequest)
        {
            await ProjectApp.CreateProjectAsync(projectRequest);
        }

        [HttpGet]
        public async Task<ProjectListResponse> ListProject()
        {
            return await ProjectApp.ListProjectAsync();
        }

        [HttpPost("{projectId}/maturitymodel/{maturidyModelId}/value/{value}")]
        public async Task SetMaturityModel([FromRoute] Guid projectId, [FromRoute] Guid maturidyModelId, [FromRoute] int value)
        {
            await ProjectApp.SetMaturityModel(new ProjectSetMaturityModelRequest { ProjectId = projectId, MaturityModelId = maturidyModelId, Value = value});
        }

        [HttpGet("{projectId}/maturitymodel")]
        public async Task<List<ProjectMaturityModelListResponse>> ListMaturityModel([FromRoute]Guid projectID)
        {
            return await ProjectApp.GetMaturityModel(projectID);
        }

        [HttpPost("{projectId}/chapters")]
        public async Task DefineChaptersProject([FromRoute]Guid projectId, ProjectChapterDefineRequest request)
        {
            request.ProjectId = projectId;
            await ProjectApp.DefineChapterAsync(request);
        }
    }
}
