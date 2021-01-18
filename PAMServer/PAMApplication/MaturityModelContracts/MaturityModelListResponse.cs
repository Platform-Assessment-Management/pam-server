using PAMApplication.MaturityModelContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.MaturityModelContracts
{
    public class MaturityModelListResponse
    {
        public Guid MaturityModelId { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public int Order { get; set; }

        public IList<OptionsDTO> Options { get; set; }
    }
}
