using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core;
using Census.Core.Interfaces;
using umbraco.BusinessLogic;
using umbraco.DataLayer;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.template;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class TemplateToTemplate : IRelation
    {

        public object From
        {
            get { return typeof(UmbracoObject.Template); }
        }

        public object To
        {
            get { return typeof(UmbracoObject.Template); }
        }

        public string Description
        {
            get { return "Templates that inherit directly from this template"; }
        }

        public DataTable GetRelations(object id)
        {
            var usages = Template.GetAllAsList().Where(t => t.MasterTemplate == (int) id);

            return UmbracoObject.Template.ToDataTable(usages);
        }

    }
}