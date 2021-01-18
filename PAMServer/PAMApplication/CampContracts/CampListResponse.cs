using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.CampContracts
{
    public class CampListResponse
    {
        public IList<CampListDTO> Camps { get; set; }
    }
}
