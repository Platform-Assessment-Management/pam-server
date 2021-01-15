using PAMDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.Projects.Exceptions
{
    public class DefineMaturityModelNotFoundException : ValidationDomainException
    {
        public DefineMaturityModelNotFoundException(Guid maturityId, ProjectDomain project) : base($"Maturity {maturityId} not found to set at project {project.Name}") { }
    }
}
