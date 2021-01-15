using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.ChapterContracts
{
    public class ChapterUpdateRequest
    {
        public Guid ChapterId { get; set; }
        public string Name { get; set; }
    }
}
