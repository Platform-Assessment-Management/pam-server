using PAMDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.MaturityModels.Exceptions
{
    public class OptionMaturityModelAlreadyExistsException : ValidationDomainException
    {
        public OptionMaturityModelAlreadyExistsException(MaturityModelDomain mmDomain, int value) : base($"Value Option {value} already exists Maturity Model {mmDomain.Name}")
        {
        }
    }
}
