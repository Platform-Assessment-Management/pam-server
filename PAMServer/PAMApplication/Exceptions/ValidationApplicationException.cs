using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.Exceptions
{
    public class ValidationApplicationException : Exception
    {
        public ValidationApplicationException(String exception) : base(exception)
        {

        }
    }
}
