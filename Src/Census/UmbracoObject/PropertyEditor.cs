using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Interfaces;
using umbraco.cms.businesslogic.datatype;

namespace Census.UmbracoObject
{
    public class PropertyEditor : IUmbracoObject
    {
        public string Name { get { return "Property Editor"; } }

        public List<string> BackofficePages
        {
            get { return new List<string>(); }
        } 

        DataTable IUmbracoObject.ToDataTable(object usages)
        {
            return ToDataTable(usages);
        }

        public static DataTable ToDataTable(object usages)
        {
            var propertyEditors = (IEnumerable<DataTypeDefinition>) usages;

            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Assembly");

            foreach (var propertyEditor in propertyEditors)
            {
                var row = dt.NewRow();
		// TODO: get usage id for link
                //row["Name"] = Helper.GenerateLink(propertyEditor.Text, "developer", string.Format("/developer/datatypes/editDataType.aspx?id={0}", usage.Id), "developerDatatype.gif");
                row["Name"] = propertyEditor.Text;
                row["Assembly"] = propertyEditor.DataType.GetType().Assembly.FullName.Split(',')[0] + ".dll";
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;

        }
    }
}