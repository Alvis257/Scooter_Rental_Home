using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ScooterRental.Exceptions;

namespace ScooterRental.Test
{
    public class RentalCalculaterTest
    {
        private IRentalCompany _target;
        private string _defaultName = "Company";
        private string _Id => "1";
        private decimal _pricePerMinute => 0.20m;
        private IScooterService _scooterService;
        private IList<RentedScooter> _rentedScooters;
        private IRentalCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _scooterService = new ScooterService();
            _rentedScooters = new List<RentedScooter>();
            _calculator = new RentalCalculator();
            _target = new RentalCompany(_defaultName, _scooterService, _rentedScooters, _calculator);
        }

        [Test]
        public void RentalCalculater_ScooterNotExsist_Should_return_0()
        {
            //Assert
            Assert.AreEqual(0, _target.CalculateIncome(null,true));
        }

        [Test]
        public void RentalCalculater_year_isNull_yearSetIsCurrent_shouldBe_80()
        {
            //Arrange
            _scooterService.AddScooter("1",_pricePerMinute);
            _scooterService.AddScooter("2", _pricePerMinute);
            _scooterService.AddScooter("3", _pricePerMinute);
            _scooterService.AddScooter("4", _pricePerMinute);

            _scooterService.GetScooterById("1").IsRented = true;
            _scooterService.GetScooterById("2").IsRented = true;
            _scooterService.GetScooterById("3").IsRented = true;
            _scooterService.GetScooterById("4").IsRented = true;

            //Act
            var startRent = DateTime.UtcNow.AddMinutes(-20);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "1",
                Price = 1,
                RentStarted = startRent,
            });
             _target.EndRent("1");
         
            _rentedScooters.Add(new RentedScooter
            {
                Id = "2",
                Price = 1,
                RentStarted = startRent,
            });
            _target.EndRent("2");

            _rentedScooters.Add(new RentedScooter
            {
                Id = "3",
                Price = 1,
                RentStarted = startRent,
            });
            _target.EndRent("3");

            _rentedScooters.Add(new RentedScooter
            {
                Id = "4",
                Price = 1,
                RentStarted = startRent,
            });
            _target.EndRent("4");

            //Assert
            Assert.AreEqual(80, _target.CalculateIncome(null, false));
        }

        [Test]
        public void RentalCalculater_includeNotCompletedRentals_True_For_All()
        {
            //Arrange
            _scooterService.AddScooter("1", _pricePerMinute);
            _scooterService.AddScooter("2", _pricePerMinute);
            _scooterService.AddScooter("3", _pricePerMinute);
            _scooterService.AddScooter("4", _pricePerMinute);

            _scooterService.GetScooterById("1").IsRented = true;
            _scooterService.GetScooterById("2").IsRented = true;
            _scooterService.GetScooterById("3").IsRented = true;
            _scooterService.GetScooterById("4").IsRented = true;

            //Act
            var startRent = DateTime.UtcNow.AddMinutes(-20);

            _rentedScooters.Add(new RentedScooter
            {
                Id = "1",
                Price = 1,
                RentStarted = startRent,
            });
           

            _rentedScooters.Add(new RentedScooter
            {
                Id = "2",
                Price = 1,
                RentStarted = startRent,
            });


            _rentedScooters.Add(new RentedScooter
            {
                Id = "3",
                Price = 1,
                RentStarted = startRent,
            });
   

            _rentedScooters.Add(new RentedScooter
            {
                Id = "4",
                Price = 1,
                RentStarted = startRent,
            });
      

            //Assert
            Assert.AreEqual(80, _target.CalculateIncome(null, true));
        }


        [Test]
        public void RentalCalculater_Given_2021_year_should_be_80()
        {
            //Arrange
            _scooterService.AddScooter("1", _pricePerMinute);
            _scooterService.AddScooter("2", _pricePerMinute);
            _scooterService.AddScooter("3", _pricePerMinute);
            _scooterService.AddScooter("4", _pricePerMinute);

            _scooterService.GetScooterById("1").IsRented = true;
            _scooterService.GetScooterById("2").IsRented = true;
            _scooterService.GetScooterById("3").IsRented = true;
            _scooterService.GetScooterById("4").IsRented = true;

            //Act
            var startRent = new DateTime(2021, 02, 03, 12, 43, 43);
            var endRent = new DateTime(2021, 02, 04, 12, 43, 43);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "1",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });
            
            _rentedScooters.Add(new RentedScooter
            {
                Id = "2",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });

            startRent = DateTime.UtcNow.AddDays(-1);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "3",
                Price = 1,
                RentStarted = startRent
            });
            _target.EndRent("3");

            _rentedScooters.Add(new RentedScooter
            {
                Id = "4",
                Price = 1,
                RentStarted = startRent
            });
            _target.EndRent("4");

            //Assert
            Assert.AreEqual(80, _target.CalculateIncome(2021, false));
        }

        [Test]
        public void RentalCalculater_Given_2022_year_should_be_80()
        {
            //Arrange
            _scooterService.AddScooter("1", _pricePerMinute);
            _scooterService.AddScooter("2", _pricePerMinute);
            _scooterService.AddScooter("3", _pricePerMinute);
            _scooterService.AddScooter("4", _pricePerMinute);

            _scooterService.GetScooterById("1").IsRented = true;
            _scooterService.GetScooterById("2").IsRented = true;
            _scooterService.GetScooterById("3").IsRented = true;
            _scooterService.GetScooterById("4").IsRented = true;

            //Act
            var startRent = new DateTime(2021, 02, 03, 12, 43, 43);
            var endRent = new DateTime(2021, 02, 04, 12, 43, 43);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "1",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });


            _rentedScooters.Add(new RentedScooter
            {
                Id = "2",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });

            startRent = DateTime.UtcNow.AddDays(-1);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "3",
                Price = 1,
                RentStarted = startRent
            });
            _target.EndRent("3");

            _rentedScooters.Add(new RentedScooter
            {
                Id = "4",
                Price = 1,
                RentStarted = startRent
            });
            _target.EndRent("4");

            //Assert
            Assert.AreEqual(80, _target.CalculateIncome(2022, false));
        }

        [Test]
        public void RentalCalculater_Given_current_year_should_be_80_includeNotCompleted_true()
        {
            //Arrange
            _scooterService.AddScooter("1", _pricePerMinute);
            _scooterService.AddScooter("2", _pricePerMinute);
            _scooterService.AddScooter("3", _pricePerMinute);
            _scooterService.AddScooter("4", _pricePerMinute);

            _scooterService.GetScooterById("1").IsRented = true;
            _scooterService.GetScooterById("2").IsRented = true;
            _scooterService.GetScooterById("3").IsRented = true;
            _scooterService.GetScooterById("4").IsRented = true;

            //Act
            var startRent = new DateTime(2021, 02, 03, 12, 43, 43);
            var endRent = new DateTime(2021, 02, 04, 12, 43, 43);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "1",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });


            _rentedScooters.Add(new RentedScooter
            {
                Id = "2",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });

            startRent = DateTime.UtcNow.AddDays(-1);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "3",
                Price = 1,
                RentStarted = startRent
            });
   
            _rentedScooters.Add(new RentedScooter
            {
                Id = "4",
                Price = 1,
                RentStarted = startRent
            });

            //Assert
            Assert.AreEqual(80, _target.CalculateIncome(DateTime.UtcNow.Year, true));
        }

        [Test]
        public void RentalCalculater_Given_current_year_should_be_80_includeNotCompleted_Given_false_when_should_be_true()
        {
            //Arrange
            _scooterService.AddScooter("1", _pricePerMinute);
            _scooterService.AddScooter("2", _pricePerMinute);
            _scooterService.AddScooter("3", _pricePerMinute);
            _scooterService.AddScooter("4", _pricePerMinute);

            _scooterService.GetScooterById("1").IsRented = true;
            _scooterService.GetScooterById("2").IsRented = true;
            _scooterService.GetScooterById("3").IsRented = true;
            _scooterService.GetScooterById("4").IsRented = true;

            //Act
            var startRent = new DateTime(2021, 02, 03, 12, 43, 43);
            var endRent = new DateTime(2021, 02, 04, 12, 43, 43);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "1",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });


            _rentedScooters.Add(new RentedScooter
            {
                Id = "2",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });

            startRent = DateTime.UtcNow.AddDays(-1);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "3",
                Price = 1,
                RentStarted = startRent
            });

            _rentedScooters.Add(new RentedScooter
            {
                Id = "4",
                Price = 1,
                RentStarted = startRent
            });

            //Assert
            Assert.AreEqual(80, _target.CalculateIncome(DateTime.UtcNow.Year, false));
        }

        [Test]
        public void RentalCalculater_Given_2021_year_should_be_80_includeNotCompleted_Given_true_when_should_be_false()
        {
            //Arrange
            _scooterService.AddScooter("1", _pricePerMinute);
            _scooterService.AddScooter("2", _pricePerMinute);
            _scooterService.AddScooter("3", _pricePerMinute);
            _scooterService.AddScooter("4", _pricePerMinute);

            _scooterService.GetScooterById("1").IsRented = true;
            _scooterService.GetScooterById("2").IsRented = true;
            _scooterService.GetScooterById("3").IsRented = true;
            _scooterService.GetScooterById("4").IsRented = true;

            //Act
            var startRent = new DateTime(2021, 02, 03, 12, 43, 43);
            var endRent = new DateTime(2021, 02, 04, 12, 43, 43);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "1",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });


            _rentedScooters.Add(new RentedScooter
            {
                Id = "2",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });

            startRent = DateTime.UtcNow.AddDays(-1);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "3",
                Price = 1,
                RentStarted = startRent
            });

            _rentedScooters.Add(new RentedScooter
            {
                Id = "4",
                Price = 1,
                RentStarted = startRent
            });

            //Assert
            Assert.AreEqual(80, _target.CalculateIncome(2021, true));
        }

        [Test]
        public void RentalCalculater_Given_null_year_should_be_160_includeNotCompleted_Given_true()
        {
            //Arrange
            _scooterService.AddScooter("1", _pricePerMinute);
            _scooterService.AddScooter("2", _pricePerMinute);
            _scooterService.AddScooter("3", _pricePerMinute);
            _scooterService.AddScooter("4", _pricePerMinute);

            _scooterService.GetScooterById("1").IsRented = true;
            _scooterService.GetScooterById("2").IsRented = true;
            _scooterService.GetScooterById("3").IsRented = true;
            _scooterService.GetScooterById("4").IsRented = true;

            //Act
            var startRent = new DateTime(2021, 02, 03, 12, 43, 43);
            var endRent = new DateTime(2021, 02, 04, 12, 43, 43);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "1",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });


            _rentedScooters.Add(new RentedScooter
            {
                Id = "2",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });

            startRent = DateTime.UtcNow.AddDays(-1);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "3",
                Price = 1,
                RentStarted = startRent
            });

            _rentedScooters.Add(new RentedScooter
            {
                Id = "4",
                Price = 1,
                RentStarted = startRent
            });

            //Assert
            Assert.AreEqual(160, _target.CalculateIncome(null, true));
        }


        [Test]
        public void RentalCalculater_Given_null_year_should_be_160_includeNotCompleted_Given_false_when_should_be_true()
        {
            //Arrange
            _scooterService.AddScooter("1", _pricePerMinute);
            _scooterService.AddScooter("2", _pricePerMinute);
            _scooterService.AddScooter("3", _pricePerMinute);
            _scooterService.AddScooter("4", _pricePerMinute);

            _scooterService.GetScooterById("1").IsRented = true;
            _scooterService.GetScooterById("2").IsRented = true;
            _scooterService.GetScooterById("3").IsRented = true;
            _scooterService.GetScooterById("4").IsRented = true;

            //Act
            var startRent = new DateTime(2021, 02, 03, 12, 43, 43);
            var endRent = new DateTime(2021, 02, 04, 12, 43, 43);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "1",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });


            _rentedScooters.Add(new RentedScooter
            {
                Id = "2",
                Price = 1,
                RentStarted = startRent,
                RentEnded = endRent
            });

            startRent = DateTime.UtcNow.AddDays(-1);
            _rentedScooters.Add(new RentedScooter
            {
                Id = "3",
                Price = 1,
                RentStarted = startRent
            });

            _rentedScooters.Add(new RentedScooter
            {
                Id = "4",
                Price = 1,
                RentStarted = startRent
            });

            //Assert
            Assert.AreEqual(160, _target.CalculateIncome(null, false));
        }

    }
}
