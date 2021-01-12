using PAMDomain.Projects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMDomain.Repositories
{
    public interface IProjectRepository
    {
        Task Save(ProjectDomain project);
        Task<ProjectDomain> Get(Guid projectId);
        Task AddMaturityModel(MaturityModelDefined maturityModelDefined);
        Task<List<MaturityModelDefined>> GetMaturityModel(Guid projectId);
        Task UpdateMaturityModel(MaturityModelDefined mm);
    }
}
