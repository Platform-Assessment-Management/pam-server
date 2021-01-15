using MongoDB.Driver;
using PAMDomain;
using PAMDomain.MaturityModels;
using PAMDomain.Repositories;
using PAMRepository.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMRepository.Impl
{
    public class MaturityModelRepository : IMaturityModelRepository
    {
        private IMongoCollection<MaturityModelDomain> maturityModelsDatabase;
        private IMongoCollection<MaturityModelProjectDomain> maturityModelsProjectDatabase;
        private IMongoCollection<ChapterDomain> maturityModelsChapterDatabase;

        public MaturityModelRepository(IPAMDatabaseSettings settings, IMongoDatabase database)
        {
            maturityModelsDatabase = database.GetCollection<MaturityModelDomain>(settings.MaturityModelsCollectionName);
            maturityModelsProjectDatabase = database.GetCollection<MaturityModelProjectDomain>(settings.MaturityProjectCollectionName);
            maturityModelsChapterDatabase = database.GetCollection<ChapterDomain>(settings.ChapterCollectionName);
        }

        public async Task<MaturityModelDomain> GetAsync(Guid maturityModelId)
        {
            return await (await maturityModelsDatabase.FindAsync(m => m.MaturityModelId == maturityModelId)).FirstOrDefaultAsync();
        }

        public async Task<IList<MaturityModelDomain>> Get(params Guid[] maturityModelId)
        {
            return await (await maturityModelsDatabase.FindAsync(m => maturityModelId.Contains(m.MaturityModelId))).ToListAsync();
        }

        public Task<IList<CampDomain>> GetCampsByChapters(Guid[] guids)
        {
            throw new NotImplementedException();
        }

        public Task<IList<MaturityModelDomain>> GetMaturitiesByChapters(params Guid[] guids)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<MaturityModelProjectDomain>> GetMaturityProjectAsync(Guid projectId)
        {
            return await (await maturityModelsProjectDatabase.FindAsync(x => x.ProjectId == projectId)).ToListAsync();
        }

        public Task<MaturityModelProjectDomain> GetProjectAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<MaturityModelDomain>> ListAsync()
        {
            return await (await maturityModelsDatabase.FindAsync(x => true)).ToListAsync();
        }

        public async Task SaveAsync(MaturityModelDomain mm)
        {
            await maturityModelsDatabase.ReplaceOneAsync(m => m.MaturityModelId == mm.MaturityModelId, mm, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<IList<ChapterDomain>> GetChaptersAsync(params Guid[] chapterIds)
        {
            return await (await maturityModelsChapterDatabase.FindAsync(c => chapterIds.Contains(c.ChapterId))).ToListAsync();
        }
    }
}
