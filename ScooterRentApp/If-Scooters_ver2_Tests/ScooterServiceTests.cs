using Microsoft.VisualStudio.TestTools.UnitTesting;
using If_Scooters_ver2;

namespace If_Scooters_ver2_Tests
{
    [TestClass]
    public class ScooterServiceTests
    {
        private IScooterService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new ScooterService();
        }

        [TestMethod]
        public void AddScooter_OneScooter_OneScooterAdded()
        {
            //Act
            //Add one scooter to scooterService's scooterList
            _service.AddScooter("id", 0.05M);
            //Expect scooterList length equals 1
            int expectedScooterListLength = 1;
            var actualScooterListLength = _service.GetScooters().Count;
            //Assert
            //Compare expected scooterList length and actual
            Assert.AreEqual(expectedScooterListLength, actualScooterListLength);
        }

        [TestMethod]
        public void Scooter_WhenMakingTwoScootersWithOneId_ShouldThrowException()
        {
            //Act
            //Add one Scooter to ScooterService's scooterList
            _service.AddScooter("id", 0.05M);
            //Assert
            //Check if adding new Scooter with same id throws an Expection
            Assert.ThrowsException<AddScooterMethodException>(() => _service.AddScooter("id", 0.05M));
        }

        [TestMethod]
        public void GetScooterById_Id_ScooterWithThisId()
        {
            //Act
            //Add one scooter to ScooterService's scooterList
            _service.AddScooter("id1", 0.05M);
            //Get scooter by id
            Scooter actualScooter = _service.GetScooterById("id1");
            //Set expected scooter's id and actual scooter's id
            string expectedScootersId = "id1";
            string actualScootersId = actualScooter.Id;
            //Assert
            //Compare expected id and actual id
            Assert.AreEqual(expectedScootersId, actualScootersId);
        }

        [TestMethod]
        public void GetScooters_ScooterList()
        {
            //Act
            //Add one scooter to ScooterService's scooterList
            _service.AddScooter("id", 0.05M);
            //Set expected scooters'id and first scooterList's elements id
            var expectedScootersId = new Scooter("id", 0.05M).Id;
            var actualScootersId = _service.GetScooters()[0].Id;
            //Assert
            //Compare expected id and actual id
            Assert.AreEqual(expectedScootersId, actualScootersId);
        }

        [TestMethod]
        public void RemoveScooter_OneScooter_EmptryScooterList()
        {
            //Act
            //Add and remove one scooter
            _service.AddScooter("id", 0.05M);
            _service.RemoveScooter("id");
            //Set expected scooterList length and actual 
            int expectedScooterListLength = 0;
            int actualScooterListLength = _service.GetScooters().Count;
            //Assert
            //Compare scooterList length's
            Assert.AreEqual(expectedScooterListLength, actualScooterListLength);
        }

        [TestMethod]
        public void GetScooterById_FakeId_ShouldThrowScooterNotFoundException()
        {
            //Assert
            Assert.ThrowsException<ScooterNotFoundException>(() => _service.GetScooterById("Fake id"));
        }

        [TestMethod]
        public void RemoveScooter_FakeId_ShouldThrowScooterNotFoundException()
        {
            //Assert
            Assert.ThrowsException<ScooterNotFoundException>(() => _service.RemoveScooter("Fake id"));
        }
    }
}
