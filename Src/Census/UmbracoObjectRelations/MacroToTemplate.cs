using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core;
using Census.Core.Interfaces;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.macro;
using umbraco.cms.businesslogic.template;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class MacroToTemplate : IRelation
    {

        public object From
        {
            get { return typeof(Macro); }
        }

        public object To
        {
            get { return typeof(Template); }
        }

        public IEnumerable<string> PagePath { get { return new List<string>() { "/developer/macros/editMacro.aspx" }; } }

        public DataTable GetRelations(object id)
        {
            var macro = Macro.GetById((int)id);

            var allTemplates = Template.GetAllAsList();

            // TODO: Can optimize these to only check for their respective syntaxes
            var usages = allTemplates.Where(template => template.Design.ToLower().Contains("alias=\"" + macro.Alias.ToLower() + "\"")).ToList();
            var mvcUsages = allTemplates.Where(template => template.Design.ToLower().Contains("rendermacro(\"" + macro.Alias.ToLower() + "\"")).ToList();

            usages.InsertRange(0, mvcUsages);

            // Convert doctypes into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Alias");

            foreach (var usage in usages)
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
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }


    }
}