using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Census.Core;

namespace Census.Web
{
    public partial class Usages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sourcePage = HttpContext.Current.Request.QueryString["sourcePage"];
            var sourceId = int.Parse(HttpContext.Current.Request.QueryString["sourceId"]);
            var relationTypes = Configuration.GetRelationsByPagePath(sourcePage);

            foreach (var relationType in relationTypes)
            {
                grid.Controls.Add(new LiteralControl(string.Format("<h2>{0}</h2>", relationType.To.ToString()).Split('.').Last())); // TODO: Cleanup
                grid.Controls.Add(DataTableToHtml(relationType.GetRelations(sourceId)));
            }
        }

        private LiteralControl DataTableToHtml(DataTable dt)
        {
            var sb = new StringBuilder();
            sb.Append("<table>");

            sb.Append("<thead>");
            sb.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sb.AppendFormat("<th>{0}</th>", column.ColumnName);
            }
            sb.Append("</tr>");
            sb.Append("</thead>");

            sb.Append("<tbody>");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.AppendFormat("<td>{0}</td>", row[i]);
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");

            sb.Append("</table>");

            return new LiteralControl(sb.ToString());
        }
    }
}