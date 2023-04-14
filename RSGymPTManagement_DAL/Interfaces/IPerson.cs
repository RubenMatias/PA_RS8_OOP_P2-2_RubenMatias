using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPTManagement_DAL.Interfaces
{

    internal interface IPerson
    {
        string FirstName { get; }
        string LastName { get; }
        string Adress { get; }
        string Email { get; }
        string PhoneNumber { get; }

    }
}
