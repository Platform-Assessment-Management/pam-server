using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMDomain
{
    public class ChapterDomain
    {
        public Guid ChapterId { get; private set; }

        public String Name { get; private set; }

        private ChapterDomain() { }

        public static async Task<ChapterDomain> CreateAsync(string name)
        {
            var chap = new ChapterDomain
            {
                ChapterId = Guid.NewGuid(),
                Name = name
            };

            var chapterRepo = UtilDomain.GetService<IChapterRepository>();
            await chapterRepo.SaveAsync(chap);

            return chap;
        }

        public async Task AlterNameAsync(string name)
        {
            this.Name = name;

            var chapterRepo = UtilDomain.GetService<IChapterRepository>();
            await chapterRepo.SaveAsync(this);
        }
    }
}
