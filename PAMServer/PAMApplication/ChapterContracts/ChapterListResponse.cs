using System;
using System.Collections.Generic;
using System.Text;

namespace PAMApplication.ChapterContracts
{
    public class ChapterListResponse
    {
        public IList<ChapterListDTO> Chapters { get; set; }
    }
}
