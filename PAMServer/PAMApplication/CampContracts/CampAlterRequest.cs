using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.CampContracts
{
    public class CampAlterRequest
    {
        public Guid CampId { get; set; }
        public String Name { get; set; }

        public String Description { get; set; }

        public int Order { get; set; }

        public Guid ChapterId { get; set; }
    }
}
