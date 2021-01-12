using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.ProjectContracts
{
    public class ProjectSetMaturityModelRequest
    {
        public Guid ProjectId { get; set; }

        public Guid MaturityModelId { get; set; }

        public int Value { get; set; }
    }
}
