using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core.Interfaces;
using umbraco;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class DataTypeToProperty : IRelation
    {

        public object From
        {
            get { return typeof(UmbracoObject.DataType); }
        }

        public object To
        {
            get { return typeof(UmbracoObject.Property); }
        }

        public DataTable GetRelations(object id)
        {
            var dataType = DataTypeDefinition.GetDataTypeDefinition((int)id);
            var usages = DocumentType.GetAllAsList().Where(x => x.PropertyTypes.Any(pt => pt.DataTypeDefinition.Id == dataType.Id));

            return UmbracoObject.Property.ToDataTable(usages, dataType.Id);
        }

        public string Description
        {
            get
            {
                return "Document Types that have a property of this Datatype";
            }
        }

    }
}