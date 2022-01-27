using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental
{
   public class RentalCalculator:IRentalCalculator
    {
        public decimal CalculateRent(RentedScooter scooter)
        {
            var time = scooter.RentEnded - scooter.RentStarted;
            return Math.Round((decimal)time.Value.TotalMinutes * scooter.Price,2);
        }
    }
}
