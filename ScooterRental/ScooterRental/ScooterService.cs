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
        private List<Scooter> _scooters;

        public ScooterService()
        {
            _scooters = new List<Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (pricePerMinute < 0)
                throw new InvalidPriceException();
            _scooters.Add(new Scooter(id,pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            if (_scooters.Any(s => s.Id == id))
                throw new ScooterNotFoundException();
            
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
