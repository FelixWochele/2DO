using _2DO_Server.Database.Data;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DO_Server.Database.Maps
{
    public class TaskListMap : ClassMap<TaskList>
    {

        public TaskListMap()
        {
            Table("Tasklists");

            Id(x => x.ID).GeneratedBy.Native();
            Map(x => x.Version).Not.Nullable();
            Map(x => x.Description).Length(50).Not.Nullable();
            Map(x => x.Comment).Length(500).Nullable();
            //HasMany(x => x.ID).KeyColumn("TasklistID"); // you were already doing this
        }
    }
}
