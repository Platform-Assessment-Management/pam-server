using PAMDomain.MaturityModels;
using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMDomain.Projects
{
    public class MaturityModelDefined
    {
        public Guid MaturityModelDefinedId { get; private set; }

        public Guid MaturityModelId { get; private set; }

        public Guid ProjectId { get; private set; }

        public int Value { get; private set; }

        public MaturityModelDefined() {
            MaturityModelDefinedId = Guid.NewGuid();
        }

        public static async Task<MaturityModelDefined> Create(ProjectDomain project, MaturityModelDomain maturity, int value)
        {
            var mm = new MaturityModelDefined {
                MaturityModelId = maturity.MaturityModelId,
                ProjectId = project.ProjectId,
                Value = value
            };

            var projectRepo = UtilDomain.GetService<IProjectRepository>();
            await projectRepo.AddMaturityModel(mm);

            return mm;
        }

        public async Task Update(int value)
        {
            Value = value;

            var projectRepo = UtilDomain.GetService<IProjectRepository>();
            await projectRepo.UpdateMaturityModel(this);
        }
    }
}
