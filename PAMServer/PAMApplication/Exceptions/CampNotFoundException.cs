using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.Exceptions
{
    public class CampNotFoundException : ValidationApplicationException
    {
        public CampNotFoundException(Guid campId) : base($"Camp {campId} not found")
        {
        }
    }
}
