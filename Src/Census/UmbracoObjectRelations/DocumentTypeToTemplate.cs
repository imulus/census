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
    public class DocumentTypeToTemplate : IRelation
    {

        public object From
        {
            get { return typeof(UmbracoObject.DocumentType); }
        }

        public object To
        {
            get { return typeof(UmbracoObject.Template); }
        }

        public DataTable GetRelations(object id)
        {
            var currentDocType = new DocumentType(int.Parse(id.ToString()));

            var templates = currentDocType.allowedTemplates;

            return UmbracoObject.Template.ToDataTable(templates);
        }

    }
}