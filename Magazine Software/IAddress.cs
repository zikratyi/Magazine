using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine_Software
{
    public interface IAddress
    {
        string City { get; set; }
        string Street { get; set; }
        string House { get; set; }
        int Flat { get; set; }
    }
}
