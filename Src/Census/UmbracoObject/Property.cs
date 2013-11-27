using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Interfaces;

namespace Census.UmbracoObject
{
    public class Property : IUmbracoObject
    {
        public string Name { get { return "Document Type"; } }

        public List<string> BackofficePages
        {
            get { return new List<string>() {"/settings/editNodeTypeNew.aspx"}; }
        } 

        DataTable IUmbracoObject.ToDataTable(object usages)
        {
            return ToDataTable(usages);
        }

        public static DataTable ToDataTable(object usages, int propertyId = 0)
        {
            var documentTypes = (IEnumerable<global::umbraco.cms.businesslogic.web.DocumentType>) usages;

            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Alias");
            dt.Columns.Add("Property");

            foreach (var documentType in documentTypes)
            {
                var row = dt.NewRow();
                row["Name"] = Helper.GenerateLink(documentType.Text, "settings", "/settings/editNodeTypeNew.aspx?id=" + documentType.Id, documentType.IconUrl);
                row["Alias"] = documentType.Alias;
                if (propertyId > 0)
                {
                    var prop = documentType.PropertyTypes.FirstOrDefault(x => x.DataTypeDefinition.Id == propertyId);
                    if (prop != null)
                        row["Property"] = prop.Name;
                }
                dt.Rows.Add(row);
                row.AcceptChanges();
            }

            return dt;
        }


    }
}