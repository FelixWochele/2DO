using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _2DO_Server.Database.Data
{

    [DataContract]
    public class TaskToCategorieRelations
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int TaskID { get; set; }
        [DataMember]
        public int CategoryID { get; set; }
        [DataMember]
        public int Version { get; set; }

    }
}
