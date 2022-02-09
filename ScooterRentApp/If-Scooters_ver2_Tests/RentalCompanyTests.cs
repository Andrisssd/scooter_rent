using Microsoft.VisualStudio.TestTools.UnitTesting;
using If_Scooters_ver2;
using System;

namespace If_Scooters_ver2_Tests
{
    [TestClass]
    public class RentalCompanyTests
    {
        private IScooterService _service;
        private DataBase _dataBase;
        private IRentalCompany _company;

        [TestInitialize]
        public void TestInitialize()
        {
            _dataBase = new DataBase();
            _service = new ScooterService();
            _company = new RentalCompany("Name", _service, _dataBase);
        }

        [TestMethod]
        public void StartRent_InitializedNonRentedScootersId_ShouldSetScootersIsRentedToTrue()
        {
            //Act
            //Add scooter and start rent for this scooter
            _service.AddScooter("id", 0.05M);
            _company.StartRent("id");
            //Assert
            //Check if rented scooter IsRented bool is true
            Assert.IsTrue(_service.GetScooterById("id").IsRented);
        }

        [TestMethod]
        public void CalculateIncome_YearAndFalse_Return20M()
        {
            //Act
            //Set rentStart and rentEnd DateTimes
            DateTime rentStart = new DateTime(2021, 10, 10, 00, 00, 00);
            DateTime rentEnd = new DateTime(2021, 10, 12, 00, 00, 00);
            //Put DateTimes to dataBase 
            _dataBase.SetDictionaryData("id", "2", 2021, 0.05M, rentStart, rentEnd);
            //Set expected TotalIncome and get actual TotalIncome for one scooter rented for 2 days 
            decimal expected = 40M;
            decimal actual = _company.CalculateIncome(2021, true);
            //Assert
            //Compare expected and actual incomes
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateIncome_YearAndTrue_Return66()
        {
            //Act
            //Set rentStart and rentEnd DateTimes
            DateTime rentStart = new DateTime(2021, 10, 10, 23, 00, 00);
            DateTime rentEnd = new DateTime(2021, 10, 14, 1, 00, 00);
            //Put DateTimes to dataBase 
            _dataBase.SetDictionaryData("id", "2", 2021, 0.05M, rentStart, rentEnd);
            //Set expected TotalIncome and get actual TotalIncome for one scooter rented for 3 days and 2 hours
            decimal expected = 66M;
            decimal actual = _company.CalculateIncome(2021, true);
            //Assert
            //Compare expected and actual incomes
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateIncome_YearAndTrue_Return0()
        {
            //Act
            //Set rentStart and rentEnd DateTimes
            DateTime rentStart = new DateTime(2021, 10, 10, 23, 00, 00);
            DateTime rentEnd = new DateTime(2021, 10, 14, 1, 00, 00);
            //Put DateTimes to dataBase 
            _dataBase.SetDictionaryData("id", "2", 2021, 0.05M, rentStart, rentEnd);
            //Set expected Income and actual income, but set CalculateIncome's year to year with no rides in database
            decimal expected = 0M;
            decimal actual = _company.CalculateIncome(2020, true);
            //Assert
            //Compare expected and actual incomes
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EndRent_OneScooterRentedFor6Hours_Return18()
        {
            //Act
            //Set rentStart to DateTime.Now - 6 hours
            DateTime rentStart = DateTime.Now - new TimeSpan(06, 00, 00);
            //Add one scooter to dataBase and set rentStart to this scooter to imitate 6 hours long ride
            _service.AddScooter("id", 0.05M);
            _service.GetScooterById("id").SetInvokeStartRentTime(rentStart);
            _dataBase.SetDictionaryData("id", "2", 2021, 0.05M, rentStart, null);
            //Set expected EndRent return value and actual return value
            decimal expected = 18M;
            decimal actual = _company.EndRent("id");
            //Assert
            //Compare expected and actual returned values
            Assert.AreEqual(expected, actual);
        }
    }
}
