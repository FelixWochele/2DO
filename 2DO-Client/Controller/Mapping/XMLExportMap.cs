using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ServiceReference1;

namespace _2DO_Client.Controller.Mapping
{
    [XmlRoot]
    public class XMLExportMap
    {
        [XmlElement("TasList")]
        public TaskList TaskList { get; set; }
        [XmlElement("Tasks")]
        public List<ServiceReference1.Task> Tasks { get; set; }

    }
}
