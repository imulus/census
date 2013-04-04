using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Census.Core.Interfaces;
using Census.UmbracoObjectRelations;

namespace Census.Core
{
    public static class Configuration
    {

        public static List<IRelation> RelationDefinitions
        {
            get
            {
                // TODO: Configurable via TypeFinder or otherwise
                return new List<IRelation>() { new DataTypeToProperty(), new TemplateToContent(), new TemplateToDocumentType() };
            }
        }

        public static List<IRelation> GetRelationsByPagePath(string pagePath)
        {
            return
                Configuration.RelationDefinitions.Where(
                    x => x.PagePath.ToLower() == pagePath.ToLower().Replace("/umbraco/", "/")).ToList();
        }
    }
}
