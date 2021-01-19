using MongoDB.Driver;
using PAMDomain;
using PAMDomain.Projects;
using PAMDomain.Repositories;
using PAMRepository.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMRepository.Impl
{
    public class ProjectRepository : IProjectRepository
    {
        private IMongoCollection<ProjectDomain> projects;
        private IMongoCollection<MaturityModelDefined> maturityProjectsDatabase;
        private IMongoCollection<ChapterDomain> chaptersProjectsDatabase;

        public ProjectRepository(IPAMDatabaseSettings settings, IMongoDatabase database)
        {
            projects = database.GetCollection<ProjectDomain>(settings.ProjectCollectionName);
            maturityProjectsDatabase = database.GetCollection<MaturityModelDefined>(settings.MaturityProjectCollectionName);
            chaptersProjectsDatabase = database.GetCollection<ChapterDomain>(settings.ChapterCollectionName);
        }

        public async Task AddMaturityModel(MaturityModelDefined maturityModelDefined)
        {
            await maturityProjectsDatabase.InsertOneAsync(maturityModelDefined);
        }

        public async Task<IList<ProjectDomain>> GetAsync(Guid? projectId)
        {
            var hasProject = projectId.HasValue;
            var projectIdValue = projectId.HasValue ? projectId.Value : new Guid();

            return await (await projects.FindAsync(p => !hasProject || p.ProjectId == projectId)).ToListAsync();
        }

        public async Task<IList<ChapterDomain>> GetChapters(IList<Guid> chaptersId)
        {
            return await (await chaptersProjectsDatabase.FindAsync<ChapterDomain>(c => chaptersId.Contains(c.ChapterId))).ToListAsync();
        }

        public async Task<List<MaturityModelDefined>> GetMaturityModel(Guid projectId)
        {
            return await (await maturityProjectsDatabase.FindAsync(m => m.ProjectId == projectId)).ToListAsync();
        }

        public async Task Save(ProjectDomain project)
        {
            await projects.ReplaceOneAsync(p => p.ProjectId == project.ProjectId, project, new ReplaceOptions { IsUpsert = true });
        }

        public async Task UpdateMaturityModel(MaturityModelDefined mm)
        {
            await maturityProjectsDatabase
                .ReplaceOneAsync(
                    Builders<MaturityModelDefined>.Filter.Eq("MaturityModelDefinedId", mm.MaturityModelDefinedId), 
                    mm, 
                    new ReplaceOptions() { IsUpsert = true }
                );
        }
    }
}
