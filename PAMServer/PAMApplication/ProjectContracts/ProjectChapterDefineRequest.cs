using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.ProjectContracts
{
    public class ProjectChapterDefineRequest
    {
        public Guid ProjectId { get; set; }

        public IList<Guid> ChaptersId { get; set; }
    }
}
