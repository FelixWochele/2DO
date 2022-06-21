using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _2DO_Server.Database.Data
{
    [DataContract]
    public class Task
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int Version { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public bool State { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public DateTime DueDate { get; set; }
        [DataMember]
        public int ReminderMinutes { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public int TasklistID { get; set; }
    }
}
