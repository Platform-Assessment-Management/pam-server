using PAMDomain.Projects;
using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PAMApplication.ProjectContracts;
using PAMApplication.ProjectContracts.DTO;
using PAMDomain.Projects.Exceptions;

namespace PAMApplication
{
    public class ProjectApplication
    {
        public IProjectRepository ProjectRepo { get; set; }

        public ProjectApplication(IProjectRepository projectRepo)
        {
            this.ProjectRepo = projectRepo;
        }

        public async Task CreateProjectAsync(ProjectCreateRequest projectRequest)
        {
            var project = await ProjectDomain.CreateProjectAsync(projectRequest.PlatformId, projectRequest.Name);

            await project.SaveAsync();
        }

        public async Task SetMaturityModel(ProjectSetMaturityModelRequest setMaturityModelRequest)
        {
            var project = (await ProjectRepo.GetAsync(setMaturityModelRequest.ProjectId)).FirstOrDefault();

            if (project == null)
                throw new ProjectNotFoundException(setMaturityModelRequest.ProjectId);

            await project.SetMaturityModel(setMaturityModelRequest.MaturityModelId, setMaturityModelRequest.Value);
        }

        public async Task<ProjectListResponse> ListProjectAsync()
        {
            var projects = await ProjectRepo.GetAsync();

            return new ProjectListResponse
            {
                Projects = projects.Select(p => new ProjectListDTO
                {
                    ProjectId = p.ProjectId,
                    PlatformId = p.PlatformId,
                    Name = p.Name,
                    ChaptersIds = p.ChaptersIds
                }).ToList()
            };
        }

        public async Task DefineChapterAsync(ProjectChapterDefineRequest request)
        {
            var project = (await ProjectRepo.GetAsync(request.ProjectId)).FirstOrDefault();

            if (project == null)
                throw new ProjectNotFoundException(request.ProjectId);

            await project.SetChaptersAsync(request.ChaptersId);
        }

        public async Task<List<ProjectMaturityModelListResponse>> GetMaturityModel(Guid projectID)
        {
            var maturityModels = await ProjectRepo.GetMaturityModel(projectID);

            return maturityModels.Select(m => new ProjectMaturityModelListResponse { MatirityModelId = m.MaturityModelId, Value = m.Value }).ToList();
        }
    }
}
