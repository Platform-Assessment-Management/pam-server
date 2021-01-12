using MongoDB.Bson.Serialization;
using PAMDomain.MaturityModels;
using PAMDomain.Platforms;
using PAMDomain.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMRepository
{
    public static class ConfigurationRepository
    {
        public static void BSONMap()
        {
            BsonClassMap.RegisterClassMap<PlatformDomain>(p => {
                p.MapIdField(x => x.PlatformId);
                p.MapMember(x => x.Name);
            });

            BsonClassMap.RegisterClassMap<ProjectDomain>(p => {
                p.MapIdField(x => x.ProjectId);
                p.MapMember(x => x.PlatformId);
                p.MapMember(x => x.Name);
            });

            BsonClassMap.RegisterClassMap<MaturityModelDefined>(p => {
                p.MapIdField(x => x.MaturityModelDefinedId);
                p.MapMember(x => x.MaturityModelId);
                p.MapMember(x => x.ProjectId);
                p.MapMember(x => x.Value);
            });

            BsonClassMap.RegisterClassMap<MaturityModelDomain>(p => {
                p.MapIdField(x => x.MaturityModelId);
                p.MapMember(x => x.Name);
                p.MapMember(x => x.Description);
                p.MapMember(x => x.Options);
            });

            BsonClassMap.RegisterClassMap<Options>(p => {
                p.MapMember(x => x.Name);
                p.MapMember(x => x.Value);
                p.MapMember(x => x.Level);
            });
        }
    }
}
