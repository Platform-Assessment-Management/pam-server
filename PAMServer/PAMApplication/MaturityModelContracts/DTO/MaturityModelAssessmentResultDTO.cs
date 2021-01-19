using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts.DTO
{
    public class MaturityModelAssessmentResultDTO
    {
        public Guid MaturityModelId { get; set; }
        public string Name { get; set; }
        public int MaxLevel { get; set; }
        public double MinimalLevel { get; set; }
        public int CurrenteLevel { get; set; }
        public bool Approved { get; set; }
        public double Level { get; set; }
    }
}
