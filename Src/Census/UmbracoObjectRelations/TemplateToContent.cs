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
    public class TemplateToContent : IRelation
    {

        public object From
        {
            get { return typeof(UmbracoObject.Template); }
        }

        public object To
        {
            get { return typeof(UmbracoObject.Content); }
        }

        public string Description
        {
            get { return "Content pages that have this template selected"; }
        }

        public DataTable GetRelations(object id)
        {
            var usages = new List<Document>();

            using (var reader = Application.SqlHelper.ExecuteReader("SELECT DISTINCT nodeId FROM cmsDocument WHERE templateID = " + id))
            {
                while (reader.Read())
                {
                    usages.Add(new Document(reader.GetInt("nodeId")));
                }
            }

            return UmbracoObject.Content.ToDataTable(usages);
        }

    }
}