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
        private IMongoCollection<MaturityModelProjectOptionDomain> maturityModelsProjectDatabase;
        private IMongoCollection<MaturityModelProjectDomain> projectDatabase;
        private IMongoCollection<ChapterDomain> maturityModelsChapterDatabase;
        private IMongoCollection<CampDomain> maturityModelsCampDatabase;

        public MaturityModelRepository(IPAMDatabaseSettings settings, IMongoDatabase database)
        {
            maturityModelsDatabase = database.GetCollection<MaturityModelDomain>(settings.MaturityModelsCollectionName);
            maturityModelsProjectDatabase = database.GetCollection<MaturityModelProjectOptionDomain>(settings.MaturityProjectCollectionName);
            projectDatabase = database.GetCollection<MaturityModelProjectDomain>(settings.ProjectCollectionName);
            maturityModelsChapterDatabase = database.GetCollection<ChapterDomain>(settings.ChapterCollectionName);
            maturityModelsCampDatabase = database.GetCollection<CampDomain>(settings.CampCollectionName);
        }

        public async Task<MaturityModelDomain> GetAsync(Guid maturityModelId)
        {
            return await (await maturityModelsDatabase.FindAsync(m => m.MaturityModelId == maturityModelId)).FirstOrDefaultAsync();
        }

        public async Task<IList<MaturityModelDomain>> GetAsync(params Guid[] maturityModelId)
        {
            return await (await maturityModelsDatabase.FindAsync(m => maturityModelId.Contains(m.MaturityModelId))).ToListAsync();
        }

        public async Task<IList<CampDomain>> GetCampsByChapters(Guid[] guids)
        {
            return await (await maturityModelsCampDatabase.FindAsync(c => guids.Contains(c.ChapterId))).ToListAsync();
        }

        public async Task<IList<MaturityModelDomain>> GetMaturitiesByChapters(params Guid[] guids)
        {
            throw new NotImplementedException();
        }

        public async Task<MaturityModelProjectDomain> GetProjectAsync(Guid projectId)
        {
            return await (await this.projectDatabase.FindAsync(m => m.ProjectId == projectId)).FirstOrDefaultAsync();
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

        public async Task<IList<CampDomain>> GetCampsAsync(Guid? campId = null)
        {
            var boolHasCamp = campId.HasValue;
            Guid campIdValue = campId.HasValue ? campId.Value : new Guid();
            return await (await this.maturityModelsCampDatabase.FindAsync(c => !boolHasCamp || c.CampId == campIdValue)).ToListAsync();
        }

        public async Task SaveCampAsync(CampDomain camp)
        {
            await this.maturityModelsCampDatabase.ReplaceOneAsync(c => c.CampId == camp.CampId, camp, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<List<MaturityModelProjectOptionDomain>> GetProjectMaturityAsync(Guid projectId)
        {
            return await (await this.maturityModelsProjectDatabase.FindAsync(m => m.ProjectId == projectId)).ToListAsync();
        }
    }
}
