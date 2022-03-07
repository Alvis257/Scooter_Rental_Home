using System;
using NUnit.Framework;
using ScooterRental.Exceptions;

namespace ScooterRental.Test
{
    public class ScooterServiceTest
    {
        private IScooterService _target;
        [SetUp]
        public void Setup()
        {
            _target = new ScooterService();
        }

        [Test]
        public void AddScooter_1_020_ScooterAdded()
        {
            //Arrange
            _target.AddScooter("1", 0.20m);

            //Assert
            Assert.AreEqual(1, _target.GetScooters().Count);
        }

        [Test]
        public void AddScooter_GetSameScooter_ScooterAdded()
        {
            //Arrange
            var id = "1";
            var pricePerMinut = 0.20m;
            _target.AddScooter(id, pricePerMinut);

            //Act
            var scooter = _target.GetScooterById(id);

            //Assert
            Assert.AreEqual(id, scooter.Id);

        }

        [Test]
        public void AddScooter_1_NegativePrice_ShouldFail()
        {
            //Arrange
            var id = "1";
            var pricePerMinute = -0.20m;

            //Assert
            Assert.Throws<InvalidPriceException>(() => _target.AddScooter(id, pricePerMinute));
        }

        [Test]
        public void AddScooter_Null_ID_ShouldFail()
        {
            //Arrange
            var pricePerMinute = 0.20m;

            //Assert
            Assert.Throws<ArgumentException>(() => _target.AddScooter(null, pricePerMinute));
        }

        [Test]
        public void AddScooter_Duplicate_ID_ShouldFail()
        {
            //Arrange
            var id = "1";
            var pricePerMinute = 0.20m;
            _target.AddScooter(id, pricePerMinute);

            //Assert
            Assert.Throws<DuplicateScooterException>(() => _target.AddScooter(id, pricePerMinute));
        }

        [Test]
        public void RemoveScooter_1_020_ScooterRemoved()
        {
            //Arrange
            var id = "1";
            var pricePerMinute = 0.20m;
            _target.AddScooter(id, pricePerMinute);

            //Act
            _target.RemoveScooter(id);

            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.GetScooterById(id));
        }

        [Test]
        public void RemoveScooter_Not_Existing_Should_Fail()
        {
            //Arrange
            var id = "1";

            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.RemoveScooter(id));
        }

        [Test]
        public void GetScooters_ChangeInventoryWithoutService_ShouldFail()
        {
            //Arrange
            var scooters = _target.GetScooters();
            scooters.Add(new Scooter("1", 0.20m));
            scooters.Add(new Scooter("2", 0.20m));
            scooters.Add(new Scooter("3", 0.20m));

            //Assert
            Assert.AreEqual(0, _target.GetScooters().Count);
        }
    }
}