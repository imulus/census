using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core;
using Census.Core.Interfaces;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.template;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class TemplateToDocumentType : IRelation
    {

        public object From 
        {
            get { return typeof (Template); }
        } // IUmbracoObject?

        public object To
        {
            get { return typeof (DocumentType); }
        }

        public IEnumerable<string> PagePath { get { return new List<string>() { "/settings/editTemplate.aspx", "/settings/views/editView.aspx" }; } }

        public DataTable GetRelations(object id)
        {
           var usages = DocumentType.GetAllAsList().Where(x => x.allowedTemplates.Any(t => t.Id == (int)id));

            // Convert doctypes into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Alias");
            dt.Columns.Add("Default?");

            foreach (var usage in usages)
            {
                var row = dt.NewRow();
                row["Name"] = Helper.GenerateLink(usage.Text, "settings", "/settings/editNodeTypeNew.aspx?id=" + usage.Id, usage.IconUrl);
                row["Alias"] = usage.Alias;
                row["Default?"] = (usage.DefaultTemplate == (int)id ? "YES" : "NO");
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }

    }
}