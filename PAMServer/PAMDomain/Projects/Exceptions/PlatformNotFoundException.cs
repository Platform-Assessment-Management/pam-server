using PAMDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.Projects.Exceptions
{
    public class PlatformNotFoundException : ValidationDomainException
    {
        public PlatformNotFoundException(Guid platformId) : base($"Platform {platformId} not found!")
        {
        }
    }
}
