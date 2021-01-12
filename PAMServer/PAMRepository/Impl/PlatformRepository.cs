using MongoDB.Driver;
using PAMDomain.Projects;
using PAMDomain.Repositories;
using PAMRepository.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PAMDomain.Platforms;

namespace PAMRepository.Impl
{
    public class PlatformRepository : IPlatformRepository
    {
        private IMongoCollection<PlatformDomain> platforms;

        public PlatformRepository(IPAMDatabaseSettings settings, IMongoDatabase database)
        {
            platforms = database.GetCollection<PlatformDomain>(settings.PlatformCollectionName);
        }

        public async Task<PlatformDomain> Get(Guid platformId)
        {
            return await (await platforms.FindAsync(x => x.PlatformId == platformId)).FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(Guid platformId)
        {
            return (await platforms.CountDocumentsAsync(x => x.PlatformId == platformId)) > 0;
        }

        public async Task Save(PlatformDomain platform)
        {
            await platforms.InsertOneAsync(platform);
        }
    }
}
