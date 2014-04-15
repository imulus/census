using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core.Interfaces;
using Lucene.Net.Search;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.media;
using umbraco.cms.businesslogic.web;
using umbraco.DataLayer;
using umbraco.DataLayer.SqlHelpers.SqlServer;

namespace Census.UmbracoObjectRelations
{
    public class MediaToContent : IRelation
    {

        public object From
        {
            get { return typeof (UmbracoObject.Media); }
        }

        public object To
        {
            get { return typeof (UmbracoObject.Content); }
        }

        public string Description
        {
            get { return "Content using this media"; }
        }

        public DataTable GetRelations(object id)
        {
            var usages = new List<Document>();


            // Media Picker
            using (var reader = Application.SqlHelper.ExecuteReader("SELECT DISTINCT contentNodeId FROM cmsPropertyData WHERE dataInt=@0", new SqlServerParameter("0", id)))
            {
                while (reader.Read())
                {
                    usages.Add(new Document(reader.GetInt("contentNodeId")));
                }
            }

            // RTE
            var media = new Media((int) id);
            if (media != null)
            {
                var file = media.getProperty("umbracoFile");
                if (file != null)
                {
                    var filePath = file.Value;

                    using (var reader = Application.SqlHelper.ExecuteReader("SELECT DISTINCT contentNodeId FROM cmsPropertyData WHERE (dataNtext LIKE '%' + @0 + '%' or dataNvarchar LIKE '%' + @0 + '%') AND contentNodeId <> @1", new SqlServerParameter("0", filePath), new SqlServerParameter("1", id)))
                    {
                        while (reader.Read())
                        {
                            usages.Add(new Document(reader.GetInt("contentNodeId")));
                        }
                    }

                }
            }

            // TODO: MNTP
            // TODO: DAMP

            return UmbracoObject.Content.ToDataTable(usages);
        }
    }
}