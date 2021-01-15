using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMDomain.Repositories
{
    public interface IChapterRepository
    {
        Task<ChapterDomain> GetAsync(Guid chapterId);
        Task SaveAsync(ChapterDomain chapter);
        Task<IList<ChapterDomain>> GetAsync();
    }
}
