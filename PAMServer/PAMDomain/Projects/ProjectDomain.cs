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

        private PlatformDomain _PlatformDomain;
        public Lazy<PlatformDomain> PlatformDomain => new Lazy<PlatformDomain>(() => LoadPlatform());

        public List<Guid> ChaptersIds { get; private set; }
        public List<ChapterDomain> Chapters { get; private set; }

        private List<MaturityModelDefined> _MaturityModels;
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
                _PlatformDomain = platformDomain
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
        }

        public async Task SaveAsync()
        {
            var projectRepo = UtilDomain.GetService<IProjectRepository>();
            await projectRepo.Save(this);
        }

        private PlatformDomain LoadPlatform()
        {
            if (_PlatformDomain == null)
            {
                var platformRepo = UtilDomain.GetService<IPlatformRepository>();
                _PlatformDomain = platformRepo.Get(this.PlatformId).Result;
            }

            return _PlatformDomain;
        }

        private List<ChapterDomain> LoadChapters()
        {
            throw new NotImplementedException();
        }

        private List<MaturityModelDefined> LoadMaturityModelDefined()
        {
            if (_MaturityModels == null)
            {
                var projectRepo = UtilDomain.GetService<IProjectRepository>();
                _MaturityModels = projectRepo.GetMaturityModel(this.ProjectId).Result;
            }

            return _MaturityModels;
        }
    }
}
