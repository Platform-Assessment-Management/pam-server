using PAMApplication.PlatformContracts;
using PAMDomain.Platforms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMApplication
{
    public class PlatformApplication
    {
        
        public async Task CreatePlatform(PlatformCreateRequest platformRequest)
        {
            var platform = PlatformDomain.CreatePlatform(platformRequest.Name);

            await platform.Save();
        }
    }
}
