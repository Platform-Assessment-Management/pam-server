using PAMApplication.ChapterContracts;
using PAMDomain;
using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMApplication
{
    public class ChapterApplication
    {
        public IChapterRepository chapterRepository { get; }

        public ChapterApplication(IChapterRepository chapterRepository)
        {
            this.chapterRepository = chapterRepository;
        }

        public async Task<ChapterCreateResponse> CreateChapterAsync(ChapterCreateRequest request)
        {
            var chapter = await ChapterDomain.CreateAsync(request.Name);

            return new ChapterCreateResponse
            {
                ChapterId = chapter.ChapterId
            };
        }

        public async Task<ChapterListResponse> ListChaptersAsync()
        {
            var chapters = await this.chapterRepository.GetAsync();

            return new ChapterListResponse
            {
                Chapters = chapters.Select(c => new ChapterListDTO
                {
                    ChapterId = c.ChapterId,
                    Name = c.Name
                }).ToList()
            };
        }

        public async Task AlterChapter(ChapterUpdateRequest request)
        {
            var chapter = await this.chapterRepository.GetAsync(request.ChapterId);

            await chapter.AlterNameAsync(request.Name);
        }
    }
}
