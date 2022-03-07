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
            decimal rentCalulated = 0.0m;
            var time = scooter.RentEnded - scooter.RentStarted;

            if (scooter.RentStarted.Date != scooter.RentEnded.Value.Date)
            {
                var first = (scooter.RentStarted.Date.AddDays(1) - scooter.RentStarted.AddHours(2));
                decimal rentFirstDay = Math.Round((decimal) first.TotalMinutes * scooter.Price, 2);
                if (rentFirstDay > 20)
                {
                    rentCalulated += 20;
                }
                else rentCalulated += rentFirstDay;
                
                var last = scooter.RentEnded - scooter.RentEnded.Value.Date;
                decimal rentLastDay = Math.Round((decimal)last.Value.TotalMinutes * scooter.Price, 2);
                if (rentLastDay > 20)
                {
                    rentCalulated += 20;
                }
                else rentCalulated += rentLastDay;

                var dayLeft = time.Value.Subtract(first).Subtract(last.Value);
                var total = (int)dayLeft.TotalDays;
                rentCalulated += total * 20;
                return rentCalulated;
            }

            rentCalulated = Math.Round((decimal)time.Value.TotalMinutes * scooter.Price,2);

            return rentCalulated > 20.0m ? 20.0m : rentCalulated;
        }

        public decimal CalculateIncome(DateTime rentStart, DateTime? rentEnd, decimal pricePerMin)
        {
            decimal rentCalulated = 0.0m;
            var time = rentEnd - rentStart; 

            if (rentStart.Date != rentEnd.Value.Date)
            {
                var first = (rentStart.Date.AddDays(1) - rentStart.AddHours(2));
                decimal rentFirstDay = Math.Round((decimal)first.TotalMinutes * pricePerMin, 2);
                if (rentFirstDay > 20)
                {
                    rentCalulated += 20;
                }
                else rentCalulated += rentFirstDay;

                var last = rentEnd - rentEnd.Value.Date;
                decimal rentLastDay = Math.Round((decimal)last.Value.TotalMinutes * pricePerMin, 2);
                if (rentLastDay > 20)
                {
                    rentCalulated += 20;
                }
                else rentCalulated += rentLastDay;

                var dayLeft = time.Value.Subtract(first).Subtract(last.Value);
                var total = (int)dayLeft.TotalDays;
                rentCalulated += total * 20;

                    return rentCalulated;
            }

            rentCalulated = Math.Round((decimal)time.Value.TotalMinutes * pricePerMin, 2);

            return rentCalulated > 20.0m ? 20.0m : rentCalulated;
        }
    }
}
