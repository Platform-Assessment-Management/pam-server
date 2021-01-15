using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.Exceptions
{
    public class MaturityModelNotFoundException : ValidationApplicationException
    {
        public MaturityModelNotFoundException(Guid maturityId) : base($"Maturity Model {maturityId} not fount")
        {
        }
    }
}
