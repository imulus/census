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
    public class DocumentTypeToDocumentType : IRelation
    {

        public object From
        {
            get { return typeof(UmbracoObject.DocumentType); }
        }

        public object To
        {
            get { return typeof(UmbracoObject.DocumentType); }
        }

        public string Description
        {
            get { return "Document Types that inherit directly from this Document Type"; }
        }

        public DataTable GetRelations(object id)
        {
            var usages = DocumentType.GetAllAsList().Where(x => x.MasterContentType == (int) id);

            return UmbracoObject.DocumentType.ToDataTable(usages);
        }

    }
}