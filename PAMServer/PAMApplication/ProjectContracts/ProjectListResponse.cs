using PAMApplication.ProjectContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.ProjectContracts
{
    public class ProjectListResponse
    {
        public IList<ProjectListDTO> Projects { get; set; }
    }
}
