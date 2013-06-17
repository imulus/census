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

        public IEnumerable<string> PagePath { get { return new List<string>() { "/developer/macros/editMacro.aspx" }; } }

        public DataTable GetRelations(object id)
        {
            var macro = Macro.GetById((int)id);
            var usages = new List<Document>();
            
            var xmlNodeByXPath = library.GetXmlNodeByXPath("/root//* [@isDoc][contains(bodyText, 'macroAlias=\"" + macro.Alias + "\"')]"); // TODO: Support legacy schema?  Unpublished?  Support non-bodyText fields
            while (xmlNodeByXPath.MoveNext())
            {
                usages.Add(new Document(int.Parse(xmlNodeByXPath.Current.GetAttribute("id", ""))));
            }

            // Convert doctypes into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");

            foreach (var usage in usages)
            {
                var row = dt.NewRow();
                row["Name"] = Helper.GenerateLink(usage.Text, "content", "/editContent.aspx?id=" + usage.Id, usage.ContentTypeIcon);
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }


    }
}