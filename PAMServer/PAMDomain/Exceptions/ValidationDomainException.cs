using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.Exceptions
{
    public class ValidationDomainException : Exception
    {
        public ValidationDomainException(String exception) : base(exception)
        {

        }
    }
}
