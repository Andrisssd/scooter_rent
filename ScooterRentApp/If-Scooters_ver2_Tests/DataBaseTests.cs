using Microsoft.VisualStudio.TestTools.UnitTesting;
using If_Scooters_ver2;
using System;

namespace If_Scooters_ver2_Tests
{
    [TestClass]
    public class DataBaseTests
    {
        private DataBase _dataBase;
        private Scooter _scooter;
        private IScooterService _service;

        [TestInitialize]
        public void DataBaseConstructor_NoParameters_NewDataBaseObject1()
        {
            _dataBase = new DataBase();
            _scooter = new Scooter("id", 0.05M);
            _service = new ScooterService();
        }

        [TestMethod]
        public void DataBaseConstructor_NoParameters_NewDataBaseObject()
        {
            //Assert
            //Check if this object exists
            Assert.IsNotNull(_dataBase);
        }

        [TestMethod]
        public void CountIncomeForScooter_OneScooterRentedFor4Hours_Return12()
        {
            //Set rentStart value to DateTime.Now - 6 hours
            DateTime rentStart = DateTime.Now - new TimeSpan(06, 00, 00);
            //Act
            //Add scooter to scooterList
            _service.AddScooter("id", 0.05M);
            //Make scooters object
            Scooter scooter = _service.GetScooterById("id");
            //Set incokeStartRentTime to rentStart and set data
            scooter.SetInvokeStartRentTime(rentStart);
            _dataBase.SetDictionaryData("id", "2", rentStart.Year, 0.05M, rentStart, null);
            //Set expected IncomeForScooter and get actual IncomeForScooter
            decimal expected = 18M;
            decimal actual = _dataBase.CountIncomeForScooter(scooter, DateTime.Now);
            //Assert
            //Compare actual and expected values
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CountTotalIncome_2021AndFalse_Return18()
        {
            //Set RentStart and RentEnd DateTimes so in total ride lasts 6 hours
            DateTime rentStart = DateTime.Now - new TimeSpan(06, 00, 00);
            DateTime rentEnd = DateTime.Now;
            //Act
            //Add one scooter to ScooterList
            _service.AddScooter("id", 0.05M);
            //Make scooters object
            Scooter scooter = _service.GetScooterById("id");
            //Set incoke start rent time and set data to dataBase Dictionary
            scooter.SetInvokeStartRentTime(rentStart);
            _dataBase.SetDictionaryData("id", "2", rentStart.Year, 0.05M, rentStart, rentEnd);
            //Set expected TotalIncome value and get actual TotalIncome value
            decimal expected = 18M;
            decimal actual = _dataBase.CountTotalIncome(rentStart.Year, false);
            //Assert
            //Compare expected and actual values
            Assert.AreEqual(expected, actual);
        }

        [TestMethod] 
        public void SetDictionaryData_Data_AddedDataToDictionary()
        {
            //Act
            //Set time to DateTime.Now
            DateTime time = DateTime.Now;
            //Set dataBase dictionary data
            _dataBase.SetDictionaryData("id", "RideNumber", 2021, 0.05M, time, time);
            //Set expected key in dataBase dictionary and expected value for this key
            string expectedKey = "id RideNumber 2021";
            string expectedValue = $"0.05&{time.ToString()}&{time.ToString()}";
            //Get actual value for dataBase data by expected key
            string actualValue = _dataBase.GetIncomeDataDictionary()[expectedKey];
            //Assert
            //Compare expected value and actual value by expected key
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void GetScootersRideTimesAndRentStartYear_OneScooterRideTimes0AndRentStart2021_Return0and2021()
        {
            //Act
            //Set invoke start rent time 
            _scooter.SetInvokeStartRentTime(new DateTime(2021, 10, 10, 10, 10, 10));
            //Set expected key in dataBase and get actual key
            string expected = "id 0 2021";
            string actual = _dataBase.GetScootersIdRideTimesAndRentStartYear(_scooter);
            //Assert
            //Compare expected and actual keys
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddScooterTime_ScooterAndScootersStartRentTime_AddScootersTimeToDictionary()
        {
            //Make new DateTime and set it to scooter's invokeStartRentTime
            DateTime time = new DateTime(2021, 10, 10, 10, 10, 10);
            //Act
            _scooter.SetInvokeStartRentTime(time);
            //Adding scooter's rent time to dataBase dictionary
            _dataBase.AddScootersTime(_scooter, time);
            //Set expected dataBase dictionary's length and get actual length
            int expectedLength = 1;
            int actualLength = _dataBase.GetIncomeDataDictionary().Count;
            //Assert
            //Compare actual and expected dataBase dictionary's length's
            Assert.AreEqual(expectedLength, actualLength);
        }

        [TestMethod]
        public void CheckDayLimitPrice_21_Return20()
        {
            //Set expected return and actual return from CheckDayLimitPrice method
            decimal expected = 20M;
            decimal actual = DataBase.CheckDayLimitPrice(21M, 20M);
            //Assert
            //Compare expected and actual returned values
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CountIncomeByDates_TimePeriod4Days_Return80()
        {
            //Set all data's needed for CountIncomeByDates method, so result rent time will be 4 days
            DateTime startRentTime = new DateTime(2021, 10, 10, 00, 00, 00);
            DateTime endRentTime = new DateTime(2021, 10, 14, 00, 00, 00);
            decimal pricePerMinute = 0.05M;
            //Set expected amount to return and get actual amount returned 
            decimal expected = 80M;
            //Act
            decimal actual = _dataBase.CountIncomeForRentalPeriod(startRentTime, endRentTime, pricePerMinute);
            //Assert
            //Compare expected and actual returned amounts
            Assert.AreEqual(expected, actual);
        }
    }
}
