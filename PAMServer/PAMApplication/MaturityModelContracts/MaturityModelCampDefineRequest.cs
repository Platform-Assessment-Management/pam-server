using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts
{
    public class MaturityModelCampDefineRequest
    {
        public Guid CampId { get; set; }
        public Guid MaturityModelId { get; set; }
        public int Level { get; set; }

    }
}
