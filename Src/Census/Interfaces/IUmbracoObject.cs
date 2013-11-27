using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Census.Interfaces
{
    public interface IUmbracoObject
    {
        string Name { get; }
        List<string> BackofficePages { get; }

        DataTable ToDataTable(object usages);
    }
}