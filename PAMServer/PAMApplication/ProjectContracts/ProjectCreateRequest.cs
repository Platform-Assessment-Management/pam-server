using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.ProjectContracts
{
    public class ProjectCreateRequest
    {
        public Guid PlatformId { get; set; }
        public String Name { get; set; }
    }
}
