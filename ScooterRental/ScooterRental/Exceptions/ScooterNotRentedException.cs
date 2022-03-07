using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Exceptions
{
    public class ScooterNotRentedException:System.Exception
    {
        public ScooterNotRentedException():base("Scooter Not Rented")
        {
            
        }
    }
}
