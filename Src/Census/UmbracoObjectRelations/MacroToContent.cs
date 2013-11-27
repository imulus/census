using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using Census.Core;
using Census.Core.Interfaces;
using umbraco;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.macro;
using umbraco.cms.businesslogic.template;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class MacroToContent : IRelation
    {

        public object From
        {
            get { return typeof(Macro); }
        }

        public object To
        {
            get { return typeof(Content); }
        }

        public DataTable GetRelations(object id)
        {
            var macro = Macro.GetById((int)id);
            var usages = new List<Document>();

            var xmlNodeByXPath = library.GetXmlNodeByXPath("/root//* [@isDoc][./* [not(@isDoc)][contains(., 'macroAlias=\"" + macro.Alias + "\"') or contains(., 'macroalias=\"" + macro.Alias + "\"')]]"); // TODO: Support legacy schema?  Unpublished?
            while (xmlNodeByXPath.MoveNext())
            {
                usages.Add(new Document(int.Parse(xmlNodeByXPath.Current.GetAttribute("id", ""))));
            }


            return UmbracoObject.Content.ToDataTable(usages);
        }

    }
}