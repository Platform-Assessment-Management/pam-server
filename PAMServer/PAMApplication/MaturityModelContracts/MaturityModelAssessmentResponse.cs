using PAMApplication.MaturityModelContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts
{
    public class MaturityModelAssessmentResponse
    {
        public List<ChapterAssessmentResultDTO> Chapters { get; set; }
    }
}
