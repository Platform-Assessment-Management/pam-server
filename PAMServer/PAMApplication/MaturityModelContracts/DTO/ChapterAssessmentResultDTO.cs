using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts.DTO
{
    public class ChapterAssessmentResultDTO
    {
        public Guid ChapterId { get; set; }
        public String Name { get; set; }

        public IList<CampsAssessmentResultDTO> Camps { get; set; }

        public Guid? CurrentCamp { get; set; }
    }
}
