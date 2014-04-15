using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Interfaces;

namespace Census.UmbracoObject
{
    public class Media : IUmbracoObject
    {
        public string Name { get { return "Media"; } }

        public List<string> BackofficePages
        {
            get
            {
                return new List<string>() { "/editMedia.aspx"};
            }
        }

        DataTable IUmbracoObject.ToDataTable(object usages)
        {
            return ToDataTable(usages);
        }

        public static DataTable ToDataTable(object usages)
        {
            var medias = (IEnumerable<umbraco.cms.businesslogic.media.Media>) usages;

            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Updated");

            foreach (var usage in medias)
            {
                var row = dt.NewRow();

                row["Name"] = usage.Text;
                row["Updated"] = usage.VersionDate;
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;

        }

    }
}