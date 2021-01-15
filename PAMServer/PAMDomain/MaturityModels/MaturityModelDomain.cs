using PAMDomain.MaturityModels.Exceptions;
using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAMDomain.MaturityModels
{
    public class MaturityModelDomain
    {
        public Guid MaturityModelId { get; private set; }

        public String Name { get; private set; }

        public String Description { get; private set; }

        public int Order { get; private set; }

        public List<OptionsDomain> Options { get; private set; }

        public List<Guid> ChaptersIds { get; private set; }

        public List<ChapterDomain> Chapters { get; private set; }

        public static async Task<MaturityModelDomain> CreateAsync(string name, string description, int order)
        {
            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();

            var mm = new MaturityModelDomain
            {
                MaturityModelId = Guid.NewGuid(),
                Name = name,
                Description = description,
                Order = order
            };

            await maturityRepo.SaveAsync(mm);

            return mm;
        }

        public async Task<OptionsDomain> IncludeOptionAsync(string name, int value, int level)
        {
            if (this.Options == null)
                this.Options = new List<OptionsDomain>();

            if (this.Options.Exists(o => o.Value == value))
                throw new OptionMaturityModelAlreadyExistsException(this, value);

            var option = OptionsDomain.CreateOption(name, value, level);

            this.Options.Add(option);

            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();
            await maturityRepo.SaveAsync(this);

            return option;
        }

        public async Task AlterOptionAsync(int value, string name, int level)
        {
            var option = this.Options.Where(o => o.Value == value).FirstOrDefault();

            if (option == null)
                throw new OptionMaturityModelNotFoundException(this, value);

            option.SetName(name);
            option.SetLevel(level);

            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();
            await maturityRepo.SaveAsync(this);
        }

        public async Task DefineChaptersAsync(IList<Guid> chaptersIds)
        {
            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();
            var chapters = await maturityRepo.GetChaptersAsync(chaptersIds.ToArray());

            var chaptersNotFound = chaptersIds.Where(c => !chapters.Any(ch => ch.ChapterId == c));

            if (chaptersNotFound != null && chaptersNotFound.Count() > 0)
                throw new ChapterMaturityModelNotFoundException(this, chaptersNotFound.ToArray());

            this.Chapters = chapters.ToList();
            this.ChaptersIds = chaptersIds.ToList();

            await maturityRepo.SaveAsync(this);
        }
    }
}
