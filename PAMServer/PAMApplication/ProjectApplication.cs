using PAMDomain.Projects;
using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PAMApplication.ProjectContracts;

namespace PAMApplication
{
    public class ProjectApplication
    {
        public IProjectRepository ProjectRepo { get; set; }

        public ProjectApplication(IProjectRepository projectRepo)
        {
            this.ProjectRepo = projectRepo;
        }

        public async Task CreateProject(ProjectCreateRequest projectRequest)
        {
            var project = await ProjectDomain.CreateProjectAsync(projectRequest.PlatformId, projectRequest.Name);

            await project.SaveAsync();
        }

        public async Task SetMaturityModel(ProjectSetMaturityModelRequest setMaturityModelRequest)
        {
            var project = await ProjectRepo.Get(setMaturityModelRequest.ProjectId);

            await project.SetMaturityModel(setMaturityModelRequest.MaturityModelId, setMaturityModelRequest.Value);
        }

        public async Task<List<ProjectMaturityModelListResponse>> GetMaturityModel(Guid projectID)
        {
            var maturityModels = await ProjectRepo.GetMaturityModel(projectID);

            return maturityModels.Select(m => new ProjectMaturityModelListResponse { MatirityModelId = m.MaturityModelId, Value = m.Value }).ToList();
        }
    }
}
