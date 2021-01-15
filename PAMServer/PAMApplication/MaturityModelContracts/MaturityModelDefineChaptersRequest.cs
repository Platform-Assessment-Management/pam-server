using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts
{
    public class MaturityModelDefineChaptersRequest
    {
        public IList<Guid> Chapters { get; set; }
        public Guid MaturityId { get; set; }
    }
}
