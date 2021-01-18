using PAMDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.Projects.Exceptions
{
    public class ProjectNotFoundException : ValidationDomainException
    {
        public ProjectNotFoundException(Guid projectId) : base($"Project {projectId} not found")
        {
        }
    }
}
