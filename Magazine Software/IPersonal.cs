using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine_Software
{
    public interface IPersonal
    {
        string LastName { get; set; }
        string FirstName { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
    }
}
