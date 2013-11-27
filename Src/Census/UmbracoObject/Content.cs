using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Census.Interfaces;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObject
{
    public class Content : IUmbracoObject
    {
        public string Name { get { return "Content"; } }

        public List<string> BackofficePages 
        {
            get
            {
                return new List<string>() {"editContent.aspx"};
            }
        }

        DataTable IUmbracoObject.ToDataTable(object usages)
        {
            return ToDataTable(usages);
        }

        public static DataTable ToDataTable(object usages)
        {
            var documents = (IEnumerable<Document>) usages;

            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Published?");
            dt.Columns.Add("Updated");

            foreach (var usage in documents)
            {
                var row = dt.NewRow();

                row["Name"] = Helper.GenerateLink(usage.Text, "content", "editContent.aspx?id=" + usage.Id, usage.ContentTypeIcon, tooltip: GetFriendlyPathForDocument(usage));
                row["Published?"] = usage.HasPublishedVersion() ? "YES" : "NO";
                row["Updated"] = usage.UpdateDate;
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }

        private static string GetFriendlyPathForDocument(Document document)
        {
            const string separator = "->";

            var nodesInPath = document.Path.Split(',').Skip(1);

            var retVal = new StringBuilder();

            foreach (var nodeId in nodesInPath)
            {
                var currentDoc = new Document(int.Parse(nodeId));

                if (currentDoc == null || string.IsNullOrEmpty(currentDoc.Path))
                    continue;

                retVal.AppendFormat("{0} {1} ", currentDoc.Text, separator);
            }

            return retVal.ToString().Substring(0, retVal.ToString().LastIndexOf(separator)).Trim();
        }

    }
}