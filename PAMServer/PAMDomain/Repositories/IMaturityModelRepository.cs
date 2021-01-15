using PAMDomain.MaturityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMDomain.Repositories
{
    public interface IMaturityModelRepository
    {
        Task<IList<MaturityModelDomain>> ListAsync();
        Task<MaturityModelDomain> GetAsync(Guid maturityModelId);
        Task<IList<MaturityModelDomain>> Get(params Guid[] maturityModelId);
        Task<MaturityModelProjectDomain> GetProjectAsync(Guid projectId);
        Task<IList<MaturityModelDomain>> GetMaturitiesByChapters(params Guid[] guids);
        Task<IList<CampDomain>> GetCampsByChapters(Guid[] guids);
        Task SaveAsync(MaturityModelDomain mm);

        Task<IList<ChapterDomain>> GetChaptersAsync(params Guid[] chapterIds);
    }
}
