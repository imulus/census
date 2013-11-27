using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Interfaces;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObject
{
    public class Template : IUmbracoObject
    {
        public string Name { get { return "Template"; } }

        public List<string> BackofficePages
        {
            get { return new List<string>() {"/settings/views/editView.aspx", "/settings/editTemplate.aspx"}; }
        } 

        DataTable IUmbracoObject.ToDataTable(object usages)
        {
            return ToDataTable(usages);
        }

        public static DataTable ToDataTable(object usages)
        {
            var templates = (IEnumerable<global::umbraco.cms.businesslogic.template.Template>) usages;

            // Convert doctypes into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Alias");

            foreach (var template in templates)
            {
                var row = dt.NewRow();
                var url = template.MasterPageFile.EndsWith("cshtml")
                              ? "/settings/views/editView.aspx?templateId=" + template.Id
                              : "/settings/editTemplate.aspx?id=" + template.Id;
                var icon = template.MasterPageFile.EndsWith("cshtml")
                               ? "settingView.gif"
                               : "settingMasterTemplate.gif";
                row["Name"] = Helper.GenerateLink(template.Text, "settings", url, icon);
                row["Alias"] = template.Alias;
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }

    }
}