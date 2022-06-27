using _2DO_Server.Database.Data;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DO_Server.Database.Maps
{
    public class TaskToCategorieRelationMap : ClassMap<TaskToCategorieRelations>
    {
        public TaskToCategorieRelationMap()
        {

            Table("TaskToCategoryRelations");
            Id(x => x.ID).GeneratedBy.Native();
            Map(x => x.Version).Not.Nullable();
            Map(x => x.CategoryID).Not.Nullable();
            Map(x => x.TaskID).Not.Nullable();
        }
    }
}
