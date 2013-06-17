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
                return new List<IRelation>() { new DataTypeToProperty(), new TemplateToContent(), new TemplateToDocumentType(), new PropertyEditorToDataType(), new MacroToTemplate(), new MacroToContent() };
            }
        }

        public static List<IRelation> GetRelationsByPagePath(string pagePath)
        {
            return
                Configuration.RelationDefinitions.Where(
                    x => x.PagePath.Any(pp => pp.ToLower() == pagePath.ToLower().Replace("/umbraco/", "/"))).ToList();
        }
    }
}
