using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Census.Core;
using Census.Interfaces;
using umbraco.cms.businesslogic.web;

namespace Census
{
    public static class Helper
    {

        public static string GenerateLink(string linkText, string sectionName, string linkUrl, string icon = "", string tooltip = "")
        {
            const string linkTemplate = "<a href='#' title=\"{3}\" onclick=\"UmbClientMgr.contentFrameAndSection('{0}', '{1}');UmbClientMgr.closeModalWindow();\">{2}</a>";

            linkUrl = string.Concat(Configuration.UmbracoDirectory, "/", linkUrl);
            var linkHtml = string.Format(linkTemplate, sectionName, linkUrl, linkText, tooltip);

            if (!string.IsNullOrEmpty(icon))
            {
                const string iconTemplate = "<img src=\"{0}{1}\" valign=\"middle\" style=\"padding-right: 5px;\" />";
                var iconHtml = string.Format(iconTemplate, Configuration.UmbracoIconDirectory, icon);

                linkHtml = string.Concat(iconHtml, linkHtml);
            }

            return linkHtml;
        }

    }
}