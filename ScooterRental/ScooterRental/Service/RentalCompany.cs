using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using ScooterRental.Exceptions;

namespace ScooterRental
{
    public class RentalCompany : IRentalCompany
    {
        public string Name { get; }
        private readonly IScooterService _scooterService;
        private readonly IList<RentedScooter> _rentedScooter;
        private readonly IRentalCalculator _calculator;

        public RentalCompany(string name, IScooterService service, 
            IList<RentedScooter> archive,IRentalCalculator calculator)
        {
            Name = name ?? throw new InvalidNameException();
            _scooterService = service;
            _rentedScooter = archive;
            _calculator = calculator;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            decimal fullIncom = 0;
            foreach (var scooter in _rentedScooter)
            {
                if (year == null)
                {
                    if (includeNotCompletedRentals == false)
                    {
                        if (scooter.RentEnded != null)
                        {
                            fullIncom +=
                                _calculator.CalculateIncome(scooter.RentStarted, scooter.RentEnded, scooter.Price);
                        }
                        else fullIncom += _calculator.CalculateIncome(scooter.RentStarted, DateTime.Now, scooter.Price);
                    }
                    else if (scooter.RentEnded != null)
                    {
                        fullIncom +=
                            _calculator.CalculateIncome(scooter.RentStarted, scooter.RentEnded, scooter.Price);
                    }
                    else fullIncom += _calculator.CalculateIncome(scooter.RentStarted, DateTime.Now, scooter.Price);
                }
                else if (Equals(year,scooter.RentStarted.Year))
                {
                    if (includeNotCompletedRentals == false)
                    {
                        if (scooter.RentEnded != null)
                        {
                            fullIncom +=
                                _calculator.CalculateIncome(scooter.RentStarted, scooter.RentEnded, scooter.Price);
                        }
                        else fullIncom += _calculator.CalculateIncome(scooter.RentStarted, DateTime.Now, scooter.Price);
                    }
                    else if (scooter.RentEnded != null)
                    {
                        fullIncom +=
                            _calculator.CalculateIncome(scooter.RentStarted, scooter.RentEnded, scooter.Price);
                    }
                    else fullIncom += _calculator.CalculateIncome(scooter.RentStarted, DateTime.Now, scooter.Price);
                }
            }

            return fullIncom;
        }

        public decimal EndRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);

            if (!scooter.IsRented)
                throw new ScooterNotRentedException();

            scooter.IsRented = false;
            var rented = _rentedScooter.First(s => s.Id == id && !s.RentEnded.HasValue);
            rented.RentEnded = DateTime.UtcNow;

            if(rented.RentStarted > rented.RentEnded)
                throw new NegativeTimeException();

            return _calculator.CalculateRent(rented);
        }

        public void StartRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            if (scooter.IsRented)
                throw new ScooterRentedException();

            scooter.IsRented = true;
            _rentedScooter.Add(new RentedScooter{
                Id = scooter.Id,
                Price = scooter.PricePerMinute,
                RentStarted = DateTime.UtcNow,
                });
        }
    }
}
