using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine_Software
{
    /// <summary>
    /// Class for save value item table List_software
    /// </summary>
    public class ListSoftware
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
