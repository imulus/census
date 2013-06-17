using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Census.Core.Interfaces
{
    public interface IRelation
    {
        object From { get; }
        object To { get; }
        IEnumerable<string> PagePath { get; }

        DataTable GetRelations(object id);
    }
}
