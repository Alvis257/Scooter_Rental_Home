using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Exceptions
{
    public class InvalidNameException:System.Exception
    {
        public InvalidNameException():base("Invalid Company Name")
        {
            
        }
    }
}
