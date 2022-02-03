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
            TimeSpan max = new TimeSpan(24, 0, 0);
            var time = scooter.RentEnded - scooter.RentStarted;
            if (time.Value.TotalMinutes > 20)
            {
                if (expr)
                {
                    
                }
            }
            return Math.Round((decimal)time.Value.TotalMinutes * scooter.Price,2);
        }

        public decimal CalculateIncome(DateTime RentStart, DateTime? RentEnd, decimal priceperminute)
        {
            throw new NotImplementedException();
        }
    }
}
