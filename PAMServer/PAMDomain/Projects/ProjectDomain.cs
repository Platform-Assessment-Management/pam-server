﻿using PAMDomain.Platforms;
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

        public Lazy<List<Chapter>> Chapters => new Lazy<List<Chapter>>(() => LoadChapters());

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
            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();

            var maturityDefined = MaturityModels.Value.FirstOrDefault(m => m.MaturityModelId == maturityModelId);

            if (maturityDefined == null)
            {
                maturityDefined = new MaturityModelDefined();
                MaturityModels.Value.Add(maturityDefined);
            }

            var maturity = await maturityRepo.Get(maturityModelId);

            if (maturity == null)
                throw new Exception();

            await MaturityModelDefined.Create(this, maturity, value);            
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

        private List<Chapter> LoadChapters()
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