using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Interfaces;

namespace Census.UmbracoObject
{
    public class Macro : IUmbracoObject
    {
        public string Name { get { return "Macro"; } }

        public List<string> BackofficePages
        {
            get { return new List<string>() {"/developer/macros/editMacro.aspx"}; }
        } 

        DataTable IUmbracoObject.ToDataTable(object usages)
        {
            return ToDataTable(usages);
        }

        public static DataTable ToDataTable(object usages)
        {
	    throw new NotImplementedException();
        }

    }
}