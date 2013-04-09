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
            umbracoPage.Load += umbracoPage_Init;
        }

        void umbracoPage_Init(object sender, EventArgs e)
        {
            var umbPage = sender as umbracoPage;

            if (umbPage == null)
                return;

            var path = umbPage.Page.Request.Path.ToLower();

            if (!Configuration.GetRelationsByPagePath(path).Any())
                return;


            AddToolbarButton(umbPage);
        }

        private void AddToolbarButton(umbracoPage page)
        {
            int pageId;
            int.TryParse(HttpContext.Current.Request.QueryString["id"], out pageId);
            
            // TODO: Hack for differing querystrings / consolidate
            if (pageId == 0)
                int.TryParse(HttpContext.Current.Request.QueryString["templateID"], out pageId);
            if (pageId == 0)
                int.TryParse(HttpContext.Current.Request.QueryString["macroID"], out pageId);

            var menu = (ScrollingMenu)Utility.FindControl<Control>((Control c) => c.ClientID.EndsWith("_menu"), page.Page);
            if (menu == null)
            {
                var tabView = (TabView) Utility.FindControl<Control>((Control c) => c.ID == "TabView1", page.Page);

                foreach (TabPage page3 in tabView.GetPanels())
                {
                    AddMenuIcon(page3.Menu, page, pageId);
                }
            }
            else
            {
                AddMenuIcon(menu, page, pageId);
            }
                

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

        private void AddMenuIcon(ScrollingMenu menu, umbracoPage page, int pageId)
        {
            MenuIconI ni = menu.NewIcon();
            ni.AltText = "View Usages";
            ni.OnClickCommand = string.Format("UmbClientMgr.openModalWindow('plugins/census/usages.aspx?sourcePage={0}&sourceId={1}', 'Usages', true, 600, 500, 0, 0); return false;", page.Request.Path, pageId);
            ni.ImageURL = "/umbraco/plugins/census/census-toolbar-icon.png"; 
        }
    }
}