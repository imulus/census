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
    public class TemplateToContent : IRelation
    {

        public object From
        {
            get { return typeof(Template); }
        }

        public object To
        {
            get { return typeof(Content); }
        }

        public IEnumerable<string> PagePath { get { return new List<string>() { "/settings/editTemplate.aspx", "/settings/views/editView.aspx" }; } }

        public DataTable GetRelations(object id)
        {
            var usages = new List<Document>();

            using (var reader = Application.SqlHelper.ExecuteReader("SELECT DISTINCT nodeId FROM cmsDocument WHERE templateID = " + id))
            {
                while (reader.Read())
                {
                    usages.Add(new Document(reader.GetInt("nodeId")));
                }
            }

            // Convert doc into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Published?");
            dt.Columns.Add("Updated");

            foreach (var usage in usages)
            {
                var row = dt.NewRow();
                row["Name"] = Helper.GenerateLink(usage.Text, "content", "/editContent.aspx?id=" + usage.Id, usage.ContentTypeIcon);
                row["Published?"] = usage.HasPublishedVersion() ? "YES" : "NO";
                row["Updated"] = usage.UpdateDate;
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }

    }
}