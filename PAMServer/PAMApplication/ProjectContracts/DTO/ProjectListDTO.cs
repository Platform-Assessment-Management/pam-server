using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.ProjectContracts.DTO
{
    public class ProjectListDTO
    {
        public Guid ProjectId { get; set; }
        public Guid PlatformId { get; set; }
        public IList<Guid> ChaptersIds { get; set; }
        public string Name { get; set; }
    }
}
