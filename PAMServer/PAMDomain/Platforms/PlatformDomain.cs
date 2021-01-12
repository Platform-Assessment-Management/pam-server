using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMDomain.Platforms
{
    public class PlatformDomain
    {
        public Guid PlatformId { get; private set; }
        public String Name { get; private set; }

        private PlatformDomain() { }

        public static PlatformDomain CreatePlatform(String name)
        {
            return new PlatformDomain
            {
                PlatformId = Guid.NewGuid(),
                Name = name
            };
        }

        public async Task Save()
        {
            var platformRepo = UtilDomain.GetService<IPlatformRepository>();

            await platformRepo.Save(this);
        }
    }
}
