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
        Task<IList<ProjectDomain>> GetAsync(Guid? projectId = null);
        Task AddMaturityModel(MaturityModelDefined maturityModelDefined);
        Task<List<MaturityModelDefined>> GetMaturityModel(Guid projectId);
        Task UpdateMaturityModel(MaturityModelDefined mm);
        Task<IList<ChapterDomain>> GetChapters(IList<Guid> chaptersId);
    }
}
