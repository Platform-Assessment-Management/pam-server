using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.MaturityModels
{
    public class MaturityModelProjectOptionDomain
    {
        public Guid MaturityModelId { get; set; }
        public int Value { get; set; }
        public Guid ProjectId { get; set; }
    }
}
