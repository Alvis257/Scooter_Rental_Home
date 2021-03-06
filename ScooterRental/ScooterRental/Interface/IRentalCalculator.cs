using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental
{
    public interface IRentalCalculator
    {
        decimal CalculateRent(RentedScooter scooter); 
        decimal CalculateIncome(DateTime RentStart, DateTime? RentEnd, decimal priceperminute);
    }
}
