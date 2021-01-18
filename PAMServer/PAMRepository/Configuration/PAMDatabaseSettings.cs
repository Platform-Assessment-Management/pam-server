using System;
using System.Collections.Generic;
using System.Text;

namespace PAMRepository.Configuration
{
    public class PAMDatabaseSettings : IPAMDatabaseSettings
    {
        public string ProjectCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string PlatformCollectionName { get; set; }
        public string MaturityModelsCollectionName { get; set; }
        public string MaturityProjectCollectionName { get; set; }
        public string ChapterCollectionName { get; set; }
        public string CampCollectionName { get; set; }
    }

    public interface IPAMDatabaseSettings
    {
        string ProjectCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string PlatformCollectionName { get; set; }

        string MaturityModelsCollectionName { get; set; }
        string MaturityProjectCollectionName { get; set; }
        string ChapterCollectionName { get; }
        string CampCollectionName { get; }
    }
}