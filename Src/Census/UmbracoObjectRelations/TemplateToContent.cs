using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core.Interfaces;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.template;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class TemplateToContent : IRelation
    {

        public object From
        {
            get { return typeof(Template); }
        } 

        public object To
        {
            get { return typeof(Document); }
        }

        public string PagePath { get { return "/settings/editTemplate.aspx"; } }

        public DataTable GetRelations(object id)
        {
            return new DataTable();
        }

    }
}