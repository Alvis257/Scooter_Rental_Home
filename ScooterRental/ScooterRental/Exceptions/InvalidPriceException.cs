using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Exceptions
{
    public class InvalidPriceException:System.Exception
    {
        public InvalidPriceException():base ("Invalid Price Provided")
        {
            
        }

        public InvalidPriceException(string message) : base(message)
        {
            
        }

        public InvalidPriceException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
