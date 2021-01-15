using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAMApplication;
using PAMApplication.ChapterContracts;

namespace PAMCore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        public ChapterApplication chapterApplication { get; }

        public ChapterController(ChapterApplication chapterApplication)
        {
            this.chapterApplication = chapterApplication;
        }

        [HttpPost]
        public async Task<ChapterCreateResponse> CreateChapter(ChapterCreateRequest request)
        {
            return await this.chapterApplication.CreateChapterAsync(request);
        }

        [HttpGet]
        public async Task<ChapterListResponse> ListChapters()
        {
            return await this.chapterApplication.ListChaptersAsync();
        }

        [HttpPut("{chapterId}")]
        public async Task AlterChapter([FromRoute]Guid chapterId, ChapterUpdateRequest request)
        {
            request.ChapterId = chapterId;

            await this.chapterApplication.AlterChapter(request);
        }
    }
}
