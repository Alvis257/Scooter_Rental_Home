using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Exceptions
{
    public class ScooterRentedException:System.Exception
    {
        public ScooterRentedException():base("Scooter is already Rented")
        {
            
        }
    }
}
