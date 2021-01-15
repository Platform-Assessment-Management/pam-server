using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.MaturityModels
{
    public class MaturityModelAssessmentResult
    {
        public IDictionary<Guid, double> CampResult { get; private set; }

        public IDictionary<Guid, double> MaturityModelResult { get; private set; }

        public void SetCamp(CampDomain camp, double value)
        {
            CampResult[camp.MaturityModeCampId] = value;
        }

        internal void SetMaturity(MaturityModelDomain mm, double value)
        {
            MaturityModelResult[mm.MaturityModelId] = value;
        }
    }
}
