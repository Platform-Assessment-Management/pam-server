using MongoDB.Driver;
using PAMDomain;
using PAMDomain.Repositories;
using PAMRepository.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMRepository.Impl
{
    public class ChapterRepository : IChapterRepository
    {
        private IMongoCollection<ChapterDomain> chapterDatabase;

        public ChapterRepository(IPAMDatabaseSettings settings, IMongoDatabase database)
        {
            chapterDatabase = database.GetCollection<ChapterDomain>(settings.ChapterCollectionName);
        }

        public async Task<ChapterDomain> GetAsync(Guid chapterId)
        {
            return await (await chapterDatabase.FindAsync(c => c.ChapterId == chapterId)).FirstOrDefaultAsync();
        }

        public async Task<IList<ChapterDomain>> GetAsync()
        {
            return await (await chapterDatabase.FindAsync(x => true)).ToListAsync();
        }

        public async Task SaveAsync(ChapterDomain chapter)
        {
            await chapterDatabase.ReplaceOneAsync(c => c.ChapterId == chapter.ChapterId, chapter, new ReplaceOptions() { IsUpsert = true });
        }
    }
}
