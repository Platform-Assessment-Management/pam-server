using PAMDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PAMDomain.MaturityModels.Exceptions
{
    public class ChapterMaturityModelNotFoundException : ValidationDomainException
    {
        public ChapterMaturityModelNotFoundException(MaturityModelDomain mmDomain, params Guid[] chapterId) : base($"Chapters {string.Concat(", ", chapterId.Select(c => c.ToString()).ToArray())} not found to set a {mmDomain.Name}")
        {
        }

        public ChapterMaturityModelNotFoundException(CampDomain campDomain, Guid chapterId) : base($"Chapter {chapterId} not found to camp {campDomain.Name}")
        {
        }
    }
}
