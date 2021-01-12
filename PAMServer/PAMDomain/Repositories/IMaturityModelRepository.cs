using PAMDomain.MaturityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMDomain.Repositories
{
    public interface IMaturityModelRepository
    {
        Task<IList<MaturityModelDomain>> List();
        Task<MaturityModelDomain> Get(Guid maturityModelId);
    }
}
