using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core.Interfaces;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class TemplateToDocumentType : IRelation
    {

        public object From 
        {
            get { return typeof (DataTypeDefinition); }
        } // IUmbracoObject?

        public object To
        {
            get { return typeof (DocumentType); }
        }

        public string PagePath { get { return "/settings/editTemplate.aspx"; } }

        public DataTable GetRelations(object id)
        {
           var usages = DocumentType.GetAllAsList().Where(x => x.allowedTemplates.Any(t => t.Id == (int)id));

            // Convert doctypes into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Icon");
            dt.Columns.Add("Alias");
            dt.Columns.Add("Default");

            foreach (var usage in usages)
            {
                var row = dt.NewRow();
                row["Name"] = usage.Text;
                row["Icon"] = usage.IconUrl;
                row["Alias"] = usage.Alias;
                row["Default"] = (usage.DefaultTemplate == (int)id ? "Y" : "N");
                row.AcceptChanges();
            }

            return dt;
        }

    }
}