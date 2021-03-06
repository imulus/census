﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Census.Core;
using Census.Core.Interfaces;
using Census.Interfaces;
using umbraco.uicontrols;

namespace Census.Web
{
    public partial class Usages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sourcePage = HttpContext.Current.Request.QueryString["sourcePage"];
            var sourceId = int.Parse(HttpContext.Current.Request.QueryString["sourceId"]);
            var umbracoObjectsForPage = Configuration.GetUmbracoObjectsByPagePath(sourcePage);
            var relationTypesForPage = Configuration.RelationDefinitions.Where(x => umbracoObjectsForPage.Select(uo => uo.GetType()).Contains(x.From));

            foreach (var relationType in relationTypesForPage)
            {
                var relations = relationType.GetRelations(sourceId);


                var tabCaption = Configuration.ResolveUmbracoObject(relationType.To).Name;

                var tabPage = TabView1.NewTabPage(tabCaption + " (" + relations.Rows.Count + ")");
                tabPage.HasMenu = false;

                if (Configuration.ShowDescriptions)
                    tabPage.Controls.Add(new LiteralControl("<span class='description'>" + relationType.Description + "</span>"));

                var pane = new Pane();
                pane.Controls.Add(DataTableToHtml(relations));
                tabPage.Controls.Add(pane);
            }
        }

        private LiteralControl DataTableToHtml(DataTable dt)
        {
            var sb = new StringBuilder();
            sb.Append("<table class='censusTable' cellpadding='0' cellspacing='0'>");

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
                for (int i = 0; i < dt.Columns.Count; i++)
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