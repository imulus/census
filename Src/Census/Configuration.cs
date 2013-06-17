using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Census.Core.Interfaces;
using Census.UmbracoObjectRelations;
using umbraco;

namespace Census.Core
{
    public static class Configuration
    {

        public static List<IRelation> RelationDefinitions
        {
            get
            {
                // TODO: Configurable via TypeFinder or otherwise
                return new List<IRelation>() { new DocumentTypeToContent(), new DocumentTypeToTemplate(), new DataTypeToProperty(), new TemplateToContent(), new TemplateToDocumentType(), new PropertyEditorToDataType(), new MacroToTemplate(), new MacroToContent() };
            }
        }

        public static List<IRelation> GetRelationsByPagePath(string pagePath)
        {
            return
                Configuration.RelationDefinitions.Where(
                    x => x.PagePath.Any(pp => pp.ToLower() == pagePath.ToLower().Replace(UmbracoDirectory.ToLower(), string.Empty))).ToList();
        }

        public static string UmbracoDirectory
        {
            get
            {
                return GlobalSettings.Path;
            }
        }

        public static object UmbracoIconDirectory
        {
            get { return string.Concat(UmbracoDirectory, "/images/umbraco/"); }
        }

        public static string PluginDirectory
        {
            get { return string.Concat(UmbracoDirectory, "/plugins/census/"); }
        }
    }
}
