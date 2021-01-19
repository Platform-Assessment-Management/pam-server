using PAMDomain.MaturityModels.Exceptions;
using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMDomain.MaturityModels
{
    public class CampDomain
    {
        public Guid CampId { get; private set; }

        public String Name { get; private set; }

        public String Description { get; private set; }

        public int Order { get; private set; }

        public List<KeyValuePair<Guid, float>> CampLevel { get; private set; }

        public Guid ChapterId { get; private set; }
        public ChapterDomain Chapter { get; private set; }

        private List<ChapterDomain> LoadChapters()
        {
            throw new NotImplementedException();
        }

        public static async Task<CampDomain> CreateAsync(string name, string description, int order, Guid chapterId)
        {
            var mmRepo = UtilDomain.GetService<IMaturityModelRepository>();

            var camp = new CampDomain
            {
                CampId = Guid.NewGuid(),
                Name = name,
                Description = description,
                Order = order
            };

            await camp.SetChapter(chapterId);

            await mmRepo.SaveCampAsync(camp);

            return camp;
        }

        public  bool ContainsMaturity(Guid maturityModelId)
        {
            return CampLevel.Any(c => c.Key == maturityModelId);
        }

        public float LevelOfMaturity(Guid maturityModelId)
        {
            return CampLevel.Where(c => c.Key == maturityModelId).Select(c => c.Value).FirstOrDefault();
        }

        public async Task AlterAsync(String? name, String? description, int? order, Guid? chapterId)
        {
            if (name != null && name.Length > 0)
                this.Name = name;

            if (description != null && description.Length > 0)
                this.Description = description;

            if (order.HasValue)
                this.Order = order.Value;

            if(chapterId.HasValue)
            {
                await SetChapter(chapterId.Value);
            }

            var mmRepo = UtilDomain.GetService<IMaturityModelRepository>();
            await mmRepo.SaveCampAsync(this);

        }

        private async Task SetChapter(Guid chapterId)
        {
            var mmRepo = UtilDomain.GetService<IMaturityModelRepository>();

            var chapter = (await mmRepo.GetChaptersAsync(chapterId)).FirstOrDefault();

            if (chapter == null)
                throw new ChapterMaturityModelNotFoundException(this, chapterId);

            this.ChapterId = chapterId;
            this.Chapter = chapter;
        }

        public async Task DefineMaturtyModelAsync(MaturityModelDomain mm, int level)
        {
            if (CampLevel == null)
                CampLevel = new List<KeyValuePair<Guid, float>>();

            CampLevel.Add(new KeyValuePair<Guid, float>(mm.MaturityModelId, level));

            var mmRepo = UtilDomain.GetService<IMaturityModelRepository>();
            await mmRepo.SaveCampAsync(this);
        }
    }
}
