using PAMDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.MaturityModels.Exceptions
{
    public class OptionMaturityModelNotFoundException : ValidationDomainException
    {
        public OptionMaturityModelNotFoundException(MaturityModelDomain mmDomain, int value) : base($"Option {value} not found at Maturity Model {mmDomain.Name}")
        {
        }
    }
}
