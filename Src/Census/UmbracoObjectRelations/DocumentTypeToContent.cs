using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core;
using Census.Core.Interfaces;
using umbraco;
using umbraco.BusinessLogic;
using umbraco.DataLayer;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.template;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class DocumentTypeToContent : IRelation
    {

        public object From
        {
            get { return typeof(UmbracoObject.DocumentType); }
        }

        public object To
        {
            get { return typeof(UmbracoObject.Content); }
        }

        public string Description
        {
            get { return "Content pages using this Document Type"; }
        }

        public DataTable GetRelations(object id)
        {
            var usages = new List<Document>();

            using (var reader = Application.SqlHelper.ExecuteReader("SELECT DISTINCT nodeId FROM cmsContent WHERE contentType = " + id))
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