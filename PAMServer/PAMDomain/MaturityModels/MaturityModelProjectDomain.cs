using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PAMDomain.MaturityModels
{
    public class MaturityModelProjectDomain
    {
        public Guid ProjectId { get; private set; }

        public List<Guid> ChaptersIds {get;private set;}

        private List<MaturityModelProjectOptionDomain> _options;
        public List<MaturityModelProjectOptionDomain> Options 
        {
            get
            {
                if(_options == null)
                {
                    var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();

                    _options = maturityRepo.GetProjectMaturityAsync(ProjectId).Result;
                }

                return _options;
            }
        }

        public int ValueOf(Guid maturityModelId)
        {
            return Options.Where(o => o.MaturityModelId == maturityModelId).Select(o => o.Value).FirstOrDefault();
        }
    }
}
