using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Census.Core;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.web;
using umbraco.presentation;
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

            if (!Configuration.RelationDefinitions.Any(x=>x.PagePath.ToLower() == path.ToLower().Replace("/umbraco/", "/")))
                return;

            int pageId;
            int.TryParse(HttpContext.Current.Request.QueryString["id"], out pageId);

            AddToolbarButton(umbPage);
        }

        private void AddToolbarButton(umbracoPage page)
        {
            var menu = (ScrollingMenu)Utility.FindControl<Control>((Control c) => c.ClientID.EndsWith("_menu"), page.Page);

            MenuIconI ni = menu.NewIcon();
            ni.AltText = "View Usages";
            ni.OnClickCommand = string.Format("UmbClientMgr.openModalWindow('plugins/census/usages.aspx?x={0}', 'Usages', true, 400, 300, 0, 0); return false;", "..");
            ni.ImageURL = "/umbraco/images/umbraco/house.png";

            string s = "<script type='text/javascript'>";
            s += "$(document).ready(function() {";
            s += @"$('.editorIcon[alt]').each(
            function() { 
                if ($(this).attr('alt').indexOf('View Usages') != -1) {
                    $(this).css('padding-bottom', '4px').css('cursor', 'pointer'); } });";
            s += "});";
            s += "</script>";
            page.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "jsfixpadding", s);

            string strCss = "<style type='text/css'>.mceToolbarExternal{padding-left: 15px;}</style>";
            page.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "cssfixtoolbar", strCss);
        }

    }
}