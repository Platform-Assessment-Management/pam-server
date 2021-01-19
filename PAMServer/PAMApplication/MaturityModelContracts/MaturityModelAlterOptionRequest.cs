using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts
{
    public class MaturityModelAlterOptionRequest
    {
        public Guid MaturityId { get; set; }

        public int value { get; set; }

        public string Name { get; set; }

        public int? Level { get; set; }
    }
}
