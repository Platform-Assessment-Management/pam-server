using PAMDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.Projects.Exceptions
{
    public class ChapterProjectNotFoundException : ValidationDomainException
    {
        public ChapterProjectNotFoundException(ProjectDomain project, params Guid[] chaptersId) : base($"Chapters {string.Concat(", ", chaptersId)} not found Project {project.PlatformDomain}")
        {
        }
    }
}
