using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

                row["Name"] = Helper.GenerateLink(usage);
                row["Published?"] = usage.HasPublishedVersion() ? "YES" : "NO";
                row["Updated"] = usage.UpdateDate;
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }

    }
}