using PAMDomain.Platforms;
using PAMDomain.Projects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMDomain.Repositories
{
    public interface IPlatformRepository
    {
        Task Save(PlatformDomain project);
        Task<PlatformDomain> Get(Guid platformId);
        Task<bool> Exists(Guid platformId);
    }
}
