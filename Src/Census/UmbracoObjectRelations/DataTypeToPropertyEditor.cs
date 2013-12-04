using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core;
using Census.Core.Interfaces;
using Census.UmbracoObject;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.web;
using umbraco.interfaces;

namespace Census.UmbracoObjectRelations
{
    public class DataTypeToPropertyEditor : IRelation
    {

        public object From
        {
            get { return typeof(UmbracoObject.DataType); }
        }

        public object To
        {
            get { return typeof(UmbracoObject.PropertyEditor); }
        }

        public DataTable GetRelations(object id)
        {
            var dataType = DataTypeDefinition.GetDataTypeDefinition((int)id);
            if (dataType == null || dataType.DataType == null)
                return new DataTable(); // PropertyEditor DLL no longer exists

            var propertyEditorGuid = dataType.DataType.Id;
            var usages = DataTypeDefinition.GetAll().Where(d => d.DataType != null && d.DataType.Id == propertyEditorGuid);

            return PropertyEditor.ToDataTable(usages);
        }

        public string Description
        {
            get { return "Property Editors (aka Render Controls) used by this Datatype"; }
        }


    }
}