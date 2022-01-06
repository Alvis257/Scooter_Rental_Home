using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScooterRental.Exceptions;

namespace ScooterRental
{
    public class ScooterService : IScooterService
    {
        public void AddScooter(string id, decimal pricePerMinute)
        {

            if (pricePerMinute < 0)
                throw new InvalidPriceException();
        }

        public void RemoveScooter(string id)
        {
        }

        public IList<Scooter> GetScooters()
        {
            throw new NotImplementedException();
        }

        public Scooter GetScooterById(string scooterId)
        {
            throw new NotImplementedException();
        }
    }
}
