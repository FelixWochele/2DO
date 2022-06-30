using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _2DO_Server.Database.Data
{
    [DataContract]
    public class Categorie
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int Version { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
