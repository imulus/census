using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core;
using Census.Core.Interfaces;
using umbraco.BusinessLogic;
using umbraco.DataLayer;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.template;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class DocumentTypeToTemplate : IRelation
    {

        public object From
        {
            get { return typeof(DocumentType); }
        }

        public object To
        {
            get { return typeof(Template); }
        }

        public IEnumerable<string> PagePath { get { return new List<string>() { "/settings/editNodeTypeNew.aspx" }; } }

        public DataTable GetRelations(object id)
        {
            var currentDocType = new DocumentType(int.Parse(id.ToString()));

            var templates = currentDocType.allowedTemplates;

            // Convert doctypes into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Alias");
            dt.Columns.Add("Default?");

            foreach (var usage in templates)
            {
                var row = dt.NewRow();
                var url = usage.MasterPageFile.EndsWith("cshtml")
                              ? "/settings/views/editView.aspx?templateId=" + usage.Id
                              : "/settings/editTemplate.aspx?id=" + usage.Id;
                var icon = usage.MasterPageFile.EndsWith("cshtml")
                               ? "settingView.gif"
                               : "settingMasterTemplate.gif";
                row["Name"] = Helper.GenerateLink(usage.Text, "settings", url, icon);
                row["Alias"] = usage.Alias;
                row["Default?"] = (currentDocType.DefaultTemplate == usage.Id ? "YES" : "NO");
                dt.Rows.Add(row);
                row.AcceptChanges();
            }
            return dt;

        }

    }
}