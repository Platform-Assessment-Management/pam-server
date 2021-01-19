using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts.DTO
{
    public class CampsAssessmentResultDTO
    {
        public Guid CampId { get; set; }
        public String Name { get; set; }
        public int Order { get; set; }
        public bool Approved { get; set; }

        public IList<MaturityModelAssessmentResultDTO> MaturityModel { get; set; }
    }
}
