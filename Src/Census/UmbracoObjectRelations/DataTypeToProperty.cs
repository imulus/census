using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core.Interfaces;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class DataTypeToProperty : IRelation
    {

        public object From
        {
            get { return typeof(DataTypeDefinition); }
        }

        public object To
        {
            get { return typeof(DocumentType); }
        }

        public IEnumerable<string> PagePath { get { return new List<string>() { "/developer/datatypes/editDataType.aspx" }; } }

        public DataTable GetRelations(object id)
        {
            var dataType = DataTypeDefinition.GetDataTypeDefinition((int)id);
            var usages = DocumentType.GetAllAsList().Where(x => x.PropertyTypes.Any(pt => pt.DataTypeDefinition.Id == dataType.Id));

            // Convert doctypes into "Relations"
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Alias");

            foreach (var usage in usages)
            {
                var row = dt.NewRow();
                row["Name"] = Helper.GenerateLink(usage.Text, "settings", string.Format("/settings/editNodeTypeNew.aspx?id={0}", usage.Id), usage.IconUrl);
                row["Alias"] = usage.Alias;
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }


    }
}