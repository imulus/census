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


            if (!Configuration.GetUmbracoObjectsByPagePath(path).Any())
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
                var tabView = (TabView)Utility.FindControl<Control>((Control c) => c.ID == "TabView1", page.Page);

                foreach (TabPage page3 in tabView.GetPanels())
                {
                    page3.Menu.InsertSplitter();
                    AddMenuIcon(page3.Menu, page, pageId);
                }
            }
            else
            {
                menu.InsertSplitter();
                AddMenuIcon(menu, page, pageId);
            }
        }

        private void AddMenuIcon(ScrollingMenu menu, umbracoPage page, int pageId)
        {
            var title = Configuration.GetUmbracoObjectsByPagePath(page.Request.Path).First().Name + " Usages";

            MenuIconI ni = menu.NewIcon();
            ni.AltText = "View Usages";
            ni.OnClickCommand = string.Format("UmbClientMgr.openModalWindow('plugins/census/usages.aspx?sourcePage={0}&sourceId={1}', '{2}', true, 600, 500, 0, 0); return false;", page.Request.Path, pageId, title);
            ni.ImageURL = string.Concat(Configuration.PluginDirectory, "census-toolbar-icon.png");

            TweakMenuButton(ref page);
        }

        private void TweakMenuButton(ref umbracoPage page)
        {
            // Fix CSS cursor
            var js = @"<script type='text/javascript'>
            $(document).ready(function() {
                $('.editorIcon[alt]').each(
                    function() { 
                        if ($(this).attr('alt').indexOf('View Usages') != -1) {
                            $(this).css('cursor', 'pointer');
                        } });
                    });
            </script>";
            page.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "censusJsFixCursor", js);

            // Fix positioning on TinyMCE pages
            var css = "<style type='text/css'>.mceToolbarExternal{padding-left: 15px;}</style>";
            page.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "censusCssFixPositioning", css);
        }

    }
}