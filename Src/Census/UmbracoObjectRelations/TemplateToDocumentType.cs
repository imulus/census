using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core;
using Census.Core.Interfaces;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.template;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class TemplateToDocumentType : IRelation
    {

        public object From
        {
            get { return typeof(UmbracoObject.Template); }
        }

        public object To
        {
            get { return typeof(UmbracoObject.DocumentType); }
        }

        public string Description
        {
            get { return "Document Types that allow this template to be selected"; }
        }

        public DataTable GetRelations(object id)
        {
            var usages = DocumentType.GetAllAsList().Where(x => x.allowedTemplates.Any(t => t.Id == (int)id));
            return UmbracoObject.DocumentType.ToDataTable(usages, (int) id);
        }

    }
}