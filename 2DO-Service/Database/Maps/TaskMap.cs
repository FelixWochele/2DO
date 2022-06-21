using FluentNHibernate.Mapping;
using _2DO_Server.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = _2DO_Server.Database.Data.Task;

namespace _2DO_Server.Database.Maps
{
    public class TaskMap : ClassMap<Task>
    { 
        public TaskMap()
        {
            Table("Task");

            Id(x => x.ID).GeneratedBy.Native();
            Map(x => x.Version).Not.Nullable();
            Map(x => x.Description).Length(100).Not.Nullable();
            Map(x => x.Comment).Length(500).Nullable();
            Map(x => x.State).Length(100).Not.Nullable();
            Map(x => x.CreationDate).Not.Nullable();
            Map(x => x.DueDate).Nullable();
            Map(x => x.ReminderMinutes).Nullable();
            Map(x => x.Priority).Not.Nullable();
            Map(x => x.TasklistID).Not.Nullable();

            References(x => x.TasklistID).Not.Nullable().Cascade.All();
        }
    }
}
