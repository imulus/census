using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core.Interfaces;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.web;
using umbraco.interfaces;

namespace Census.UmbracoObjectRelations
{
    public class PropertyEditorToDataType : IRelation
    {

        public object From 
        {
            get { return typeof (IDataType); } // RenderControl/PropertyEditor...
        }

        public object To
        {
            get { return typeof (DataTypeDefinition); }
        }

        public IEnumerable<string> PagePath { get { return new List<string>() { "/developer/datatypes/editDataType.aspx" }; } }

        public DataTable GetRelations(object id)
        {
            var dataType = DataTypeDefinition.GetDataTypeDefinition((int)id);
            var propertyEditorGuid = dataType.DataType.Id;
            var usages = DataTypeDefinition.GetAll().Where(d => d.DataType.Id == propertyEditorGuid);

            var dt = new DataTable();
            dt.Columns.Add("Name");

            foreach (var usage in usages)
            {
                var row = dt.NewRow();
                row["Name"] = usage.Text;
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }


    }
}