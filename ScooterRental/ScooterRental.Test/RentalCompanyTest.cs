using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ScooterRental.Exceptions;

namespace ScooterRental.Test
{
    public class RentalCompanyTest
    {
        private IRentalCompany _target;
        private string _defaultName = "Company";
        private IScooterService _scooterService;
        private string _Id => "1";
        private decimal _pricePerMinute => 0.20m;
        private IList<RentedScooter> _rentedScooters;
        private IRentalCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _scooterService = new ScooterService();
            _rentedScooters = new List<RentedScooter>();
            _calculator = new RentalCalculator();
            _target = new RentalCompany(_defaultName, _scooterService, _rentedScooters,_calculator);
        }

        [Test]
        public void Company_Name_Should_Be_Company_NotNull()
        {
            //Arrange
            _target = new RentalCompany(_defaultName, _scooterService, _rentedScooters,_calculator);

            //Assert
            Assert.AreEqual(_defaultName, _target.Name);
        }

        [Test]
        public void Company_NullNameGiven_Should_Throw_InvalidNameException()
        {
            //Assert
            Assert.Throws<InvalidNameException>(() =>
                _target = new RentalCompany(null, _scooterService, _rentedScooters,_calculator));
        }

        [Test]
        public void Rent_StartRent_First_Scooter_Should_Be_Rented()
        {
            //Arrange
            _scooterService.AddScooter(_Id, 0.20m);

            //Act
            _target.StartRent(_Id);

            //Assert
            var scooter = _scooterService.GetScooterById(_Id);
            Assert.AreEqual(true, scooter.IsRented);
        }

        [Test]
        public void Rent_StartRent_RentRentedScooter_ScooterRentedException()
        {
            //Arrange
            _scooterService.AddScooter(_Id, 0.20m);

            //Act
            _target.StartRent(_Id);

            //Assert
            Assert.Throws<ScooterRentedException>(() => _target.StartRent(_Id));
        }

        [Test]
        public void Rent_StartRent_RentNotExsistingScooter_ScooterNotFoundException()
        {
            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.StartRent(_Id));
        }

        [Test]                                                                                          
        public void StartRent_FirstScooterRented_RentedListShouldBeUpdated()
        {
            //Arrange
            _scooterService.AddScooter(_Id, _pricePerMinute);

            //Act
            _target.StartRent(_Id);
            var scooter = _rentedScooters.First(s=>s.Id == _Id);
            //Assert
            Assert.AreEqual(1,_rentedScooters.Count);
            Assert.AreEqual(_pricePerMinute,scooter.Price);
        }

        [Test]
        public void Rent_EndRent_EndRentFirstScooter_Should_Pass()
        {
            //Arrange
            _scooterService.AddScooter(_Id, _pricePerMinute);

            //Act
            _target.StartRent(_Id);
            var result = _target.EndRent(_Id);

            //Assert
            Assert.GreaterOrEqual(result, 0);
        }

        [Test]
        public void Rent_EndRent_EndRentNotRentedScooter_Should_Should_Throw_Not_Rented_Scooter()
        {
            //Arrange
            _scooterService.AddScooter(_Id, _pricePerMinute);

            //Assert
            Assert.Throws<ScooterNotRentedException>(() => _target.EndRent(_Id));
        }

        [Test]
        public void EndRent_FirstScooterRented_RentedListShouldBeUpdated()
        {
            //Arrange
            _scooterService.AddScooter(_Id, _pricePerMinute);
            _target.StartRent(_Id);
            //Act
            _target.EndRent(_Id);
            var scooter = _rentedScooters.FirstOrDefault(s => s.Id == _Id);
            //Assert
            Assert.AreEqual(1, _rentedScooters.Count);
            Assert.NotNull( scooter.RentEnded);
        }

        [Test]

        public void EndRent_ScooterRented20Minutes_Should_Return20()
        {
            //Arrange
            _scooterService.AddScooter(_Id,1);
            _scooterService.GetScooterById(_Id).IsRented = true;
            var startRent = DateTime.UtcNow.AddMinutes(-20);
            _rentedScooters.Add(new RentedScooter
            {
                Id = _Id,
                Price = 1,
                RentStarted = startRent,
            });

            //Act
            var result = _target.EndRent(_Id);

            //Assert
            Assert.AreEqual(20.0m,result);
        }
    }
}
