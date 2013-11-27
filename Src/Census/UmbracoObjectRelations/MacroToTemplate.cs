using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Census.Core;
using Census.Core.Interfaces;
using umbraco.cms.businesslogic.datatype;
using umbraco.cms.businesslogic.macro;
using umbraco.cms.businesslogic.template;
using umbraco.cms.businesslogic.web;

namespace Census.UmbracoObjectRelations
{
    public class MacroToTemplate : IRelation
    {

        public object From
        {
            get { return typeof(UmbracoObject.Macro); }
        }

        public object To
        {
            get { return typeof(UmbracoObject.Template); }
        }

        public DataTable GetRelations(object id)
        {
            var macro = Macro.GetById((int)id);

            var allTemplates = Template.GetAllAsList();

            // TODO: Can optimize these to only check for their respective syntaxes
            var usages = allTemplates.Where(template => template.Design.ToLower().Contains("alias=\"" + macro.Alias.ToLower() + "\"")).ToList();
            var mvcUsages = allTemplates.Where(template => template.Design.ToLower().Contains("rendermacro(\"" + macro.Alias.ToLower() + "\"")).ToList();

            usages.InsertRange(0, mvcUsages);

            return UmbracoObject.Template.ToDataTable(usages);
        }

    }
}