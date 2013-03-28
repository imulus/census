using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.web;
using umbraco.presentation.LiveEditing;
using umbraco.presentation.masterpages;
using umbraco.uicontrols;

namespace Census.Events
{
    public class PageEvents : ApplicationBase
    {

        public PageEvents()
        {
            umbracoPage.Init += umbracoPage_Init;
        }

        void umbracoPage_Init(object sender, EventArgs e)
        {
            var umbPage = sender as umbracoPage;

            if (umbPage == null)
                return;

            var path = umbPage.Page.Request.Path.ToLower();

            if (!path.Contains("editdatatype.aspx"))
                return;

            int pageId;
            int.TryParse(HttpContext.Current.Request.QueryString["id"], out pageId);

            // Dirty, POC code for showing DataType usages
            var dataType = DataTypeDefinition.GetDataTypeDefinition(pageId);
            var usages =
                DocumentType.GetAllAsList().Where(x => x.PropertyTypes.Any(pt => pt.DataTypeDefinition.Id == dataType.Id));

            var sb = new StringBuilder();
            sb.Append("<table>");
            foreach (var usage in usages)
            {
                sb.AppendFormat("<tr><td>{0}</td></tr>", usage.Text);
            }
            sb.Append("</table>");

            var pageBody = Utility.FindControl<Control>((Control c) => c.ClientID.EndsWith("Panel1"), umbPage.Page);
            pageBody.Controls.Add(new LiteralControl("<h2 class='propertypaneTitel'>Usages</h2>"));

            var pane = new Pane();
            pane.Controls.Add(new LiteralControl(sb.ToString()));
            pageBody.Controls.Add(pane);
        }

    }
}