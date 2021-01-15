using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.MaturityModels
{
    public class MaturityModelProjectDomain
    {
        public Guid ProjectId { get; private set; }

        public List<Guid> Chapters {get;private set;}

        public List<KeyValuePair<Guid, int>> Maturities { get; private set; }
    }
}
