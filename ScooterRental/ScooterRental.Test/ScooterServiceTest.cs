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
            _target.AddScooter("1",0.20m); 
            //Assert
            Assert.Pass();
        }

        [Test]
        public void AddScooter_1_negativePrice_ShouldFail()
        {
            //Arrange
            var id = "1";
            var pricePerMinute = -0.20m;
            //Assert
            Assert.Throws<InvalidPriceException>(()=>_target.AddScooter(id,pricePerMinute));
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
            Assert.Pass();
        }
    }
}