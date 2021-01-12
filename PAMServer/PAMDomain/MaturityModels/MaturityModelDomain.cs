using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain.MaturityModels
{
    public class MaturityModelDomain
    {
        public Guid MaturityModelId { get; private set; }

        public String Name { get; private set; }

        public String Description { get; private set; }

        public int Order { get; private set; }

        public List<Options> Options { get; private set; }

        public Lazy<List<Chapter>> Chapters => new Lazy<List<Chapter>>(() => LoadChapters());

        private List<Chapter> LoadChapters()
        {
            throw new NotImplementedException();
        }
    }
}
