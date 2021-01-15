using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.MaturityModels
{
    public class CampDomain
    {
        public Guid MaturityModeCampId { get; private set; }

        public String Name { get; private set; }

        public String Description { get; private set; }

        public int Order { get; private set; }

        public List<KeyValuePair<Guid, float>> CampLevel { get; private set; }

        public ChapterDomain Chapter { get; private set; }

        private List<ChapterDomain> LoadChapters()
        {
            throw new NotImplementedException();
        }

    }
}
