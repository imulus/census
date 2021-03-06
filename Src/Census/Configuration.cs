using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Census.Core.Interfaces;
using Census.Interfaces;
using Census.UmbracoObject;
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
                return new List<IRelation>() { new DocumentTypeToContent(), new DocumentTypeToDocumentType(), new DocumentTypeToTemplate(), new DataTypeToProperty(), new TemplateToContent(), new TemplateToDocumentType(), new DataTypeToPropertyEditor(), new MacroToTemplate(), new MacroToContent(), new TemplateToTemplate(), new MediaToContent()};
            }
        }

        public static List<IUmbracoObject> UmbracoObjects
        {
            get
            {
                return new List<IUmbracoObject>() { new Content(), new DataType(), new DocumentType(), new Macro(), new Media(), new Property(), new PropertyEditor(), new Template() };
            }
        } 

        public static List<IUmbracoObject> GetUmbracoObjectsByPagePath(string pagePath)
        {
            return Configuration.UmbracoObjects.Where(
                x =>
                    x.BackofficePages.Any(
                        bp => bp.ToLower() == pagePath.ToLower().Replace(UmbracoDirectory.ToLower(), string.Empty))).ToList();
            
        }

        public static IUmbracoObject ResolveUmbracoObject(object umbracoObjectType)
        {
            var typeName = ((Type) umbracoObjectType).Name;
            return UmbracoObjects.FirstOrDefault(o => o.GetType().Name == typeName);
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

        public static bool ShowDescriptions
        {
            get { return true; }
        }

    }
}
