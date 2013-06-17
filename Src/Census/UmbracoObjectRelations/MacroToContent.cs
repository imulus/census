using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using Census.Core.Interfaces;
using umbraco;
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
            get { return typeof(Document); }
        }

        public IEnumerable<string> PagePath { get { return new List<string>() { "/developer/macros/editMacro.aspx" }; } }

        public DataTable GetRelations(object id)
        {
            var macro = Macro.GetById((int)id);

            var allTemplates = Template.GetAllAsList();
            var usages = new List<Document>();
            
            var xmlNodeByXPath = library.GetXmlNodeByXPath("/root//* [contains(data  ,'macroAlias=\"" + macro.Alias + "\"')]"); // TODO: Legacy schema?
            while (xmlNodeByXPath.MoveNext())
            {
                usages.Add(new Document(int.Parse(xmlNodeByXPath.Current.GetAttribute("id", ""))));
            }

            // Convert doctypes into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Id");

            foreach (var usage in usages)
            {
                var row = dt.NewRow();
                row["Name"] = usage.Text;
                row["Alias"] = usage.Id;
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }


    }
}