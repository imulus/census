using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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

        public string PagePath { get { return "/developer/macros/editMacro.aspx"; } }

        public DataTable GetRelations(object id)
        {
            var macro = Macro.GetById((int) id);

            var allTemplates = Template.GetAllAsList();
            var usages = allTemplates.Where(template => template.Design.ToLower().Contains("alias=\"" + macro.Alias.ToLower() + "\"")).ToList(); // Need to check for View syntax also

            // Convert doctypes into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Alias");

            foreach (var usage in usages)
            {
                var row = dt.NewRow();
                row["Name"] = usage.Text;
                row["Alias"] = usage.Alias;
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }


    }
}