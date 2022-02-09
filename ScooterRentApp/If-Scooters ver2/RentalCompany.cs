using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace If_Scooters_ver2
{
    public class RentalCompany : IRentalCompany
    {
        private IScooterService ScooterService;
        private DataBase _incomeDataBase;

        public RentalCompany(string name, IScooterService service)
        {
            ScooterService = service;
            _incomeDataBase = new DataBase();
            Name = name;
        }

        public RentalCompany(string name, IScooterService service, DataBase dataBase)
        {
            ScooterService = service;
            _incomeDataBase = dataBase;
            Name = name;
        }

        public string Name { get; }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            return _incomeDataBase.CountTotalIncome(year, includeNotCompletedRentals);
        }

        public decimal EndRent(string id)
        {
            Scooter scooter = ScooterService.GetScooterById(id);
            scooter.IsRented = false;
            _incomeDataBase.AddScootersTime(scooter, DateTime.Now);
            return Math.Floor(_incomeDataBase.CountIncomeForScooter(scooter, DateTime.Now));
        }

        public void StartRent(string id)
        {
            Scooter scooter = ScooterService.GetScooterById(id);
            scooter.IsRented = true;
            _incomeDataBase.IncrementScootersRideTimes(id);
            scooter.SetInvokeStartRentTime(DateTime.Now);
            _incomeDataBase.AddScootersTime(scooter, DateTime.Now);
        }
    }
}
