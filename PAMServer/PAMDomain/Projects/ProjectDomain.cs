using PAMDomain.Platforms;
using PAMDomain.Projects.Exceptions;
using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMDomain.Projects
{
    public class ProjectDomain
    {
        public Guid ProjectId { get; private set; }
        public Guid PlatformId { get; private set; }
        public String Name { get; private set; }

        private PlatformDomain _platformDomain;
        public Lazy<PlatformDomain> PlatformDomain => new Lazy<PlatformDomain>(() => LoadPlatform());

        public IList<Guid> ChaptersIds { get; private set; }

        private IList<ChapterDomain> _chapters;
        public Lazy<IList<ChapterDomain>> Chapters => new Lazy<IList<ChapterDomain>>(() => LoadChapters());

        private List<MaturityModelDefined> _maturityModels;
        public Lazy<List<MaturityModelDefined>> MaturityModels => new Lazy<List<MaturityModelDefined>>(() => LoadMaturityModelDefined());

        

        private ProjectDomain() { }

        public static async Task<ProjectDomain> CreateProjectAsync(Guid platformId, String name)
        {
            var platformRepo = UtilDomain.GetService<IPlatformRepository>();
            var platformDomain = await platformRepo.Get(platformId);

            if (platformDomain == null)
                throw new PlatformNotFoundException(platformId);

            var p = new ProjectDomain { 
                ProjectId = Guid.NewGuid(),
                PlatformId = platformId,
                Name = name,
                _platformDomain = platformDomain
            };

            return p;
        }

        public async Task SetMaturityModel(Guid maturityModelId, int value)
        {
            var maturityDefined = MaturityModels.Value.FirstOrDefault(m => m.MaturityModelId == maturityModelId);

            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();
            var maturity = await maturityRepo.GetAsync(maturityModelId);

            if (maturity == null)
                throw new DefineMaturityModelNotFoundException(maturityModelId, this);

            if(!maturity.Options.Exists(o => o.Value == value))
                throw new DefineOptionMaturityModelNotFoundException(this, maturity, value);

            if (maturityDefined == null)
            {
                maturityDefined = await MaturityModelDefined.CreateAsync(this, maturity, value);
                MaturityModels.Value.Add(maturityDefined);
            } else
            {
                await maturityDefined.UpdateAsync(value);
            }
        }

        public async Task SetChaptersAsync(IList<Guid> chaptersId)
        {
            var projectRepo = UtilDomain.GetService<IProjectRepository>();
            var chapters = await projectRepo.GetChapters(chaptersId);

            var chaptersNotFound = chaptersId.Where(ci => !chapters.Any(c => c.ChapterId == ci)).ToArray();
            if (chaptersNotFound.Length > 0)
                throw new ChapterProjectNotFoundException(this, chaptersNotFound);

            this.ChaptersIds = chaptersId;
            this._chapters = chapters;

            await projectRepo.Save(this);
        }

        public async Task SaveAsync()
        {
            var projectRepo = UtilDomain.GetService<IProjectRepository>();
            await projectRepo.Save(this);
        }

        private PlatformDomain LoadPlatform()
        {
            if (_platformDomain == null)
            {
                var platformRepo = UtilDomain.GetService<IPlatformRepository>();
                _platformDomain = platformRepo.Get(this.PlatformId).Result;
            }

            return _platformDomain;
        }

        private IList<ChapterDomain> LoadChapters()
        {
            if(_chapters == null)
            {
                var projectRepo = UtilDomain.GetService<IProjectRepository>();
                _chapters = projectRepo.GetChapters(ChaptersIds).Result;
            }

            return _chapters;
        }

        private List<MaturityModelDefined> LoadMaturityModelDefined()
        {
            if (_maturityModels == null)
            {
                var projectRepo = UtilDomain.GetService<IProjectRepository>();
                _maturityModels = projectRepo.GetMaturityModel(this.ProjectId).Result;
            }

            return _maturityModels;
        }
    }
}
