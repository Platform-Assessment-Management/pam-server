using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts
{
    public class MaturityModelIncludeOptionRequest
    {
        public Guid MaturityId { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public int Level { get; set; }
    }
}
