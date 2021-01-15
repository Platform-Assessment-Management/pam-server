using PAMDomain.Exceptions;
using PAMDomain.MaturityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.Projects.Exceptions
{
    public class DefineOptionMaturityModelNotFoundException : ValidationDomainException
    {
        public DefineOptionMaturityModelNotFoundException(ProjectDomain project, MaturityModelDomain model, int value) : base($"Not found value {value} Maturity Model {model.Name} at project {project.Name}")
        {
        }
    }
}
