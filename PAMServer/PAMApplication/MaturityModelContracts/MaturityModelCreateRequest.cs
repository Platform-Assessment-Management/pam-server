using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts
{
    public class MaturityModelCreateRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }
    }
}
