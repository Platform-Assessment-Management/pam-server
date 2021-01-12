using MongoDB.Driver;
using PAMDomain.MaturityModels;
using PAMDomain.Repositories;
using PAMRepository.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMRepository.Impl
{
    public class MaturityModelRepository : IMaturityModelRepository
    {
        private IMongoCollection<MaturityModelDomain> maturotyModelsDatabase;

        public MaturityModelRepository(IPAMDatabaseSettings settings, IMongoDatabase database)
        {
            maturotyModelsDatabase = database.GetCollection<MaturityModelDomain>(settings.MaturityModelsCollectionName);
        }

        public async Task<MaturityModelDomain> Get(Guid maturityModelId)
        {
            return await (await maturotyModelsDatabase.FindAsync(m => m.MaturityModelId == maturityModelId)).FirstOrDefaultAsync();
        }

        public async Task<IList<MaturityModelDomain>> List()
        {
            return await (await maturotyModelsDatabase.FindAsync(x => true)).ToListAsync();
        }
    }
}
