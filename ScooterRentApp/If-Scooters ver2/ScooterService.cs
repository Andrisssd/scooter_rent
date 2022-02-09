using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace If_Scooters_ver2
{
    public class ScooterService : IScooterService
    {
        private List<Scooter> _scooterList;

        public ScooterService()
        {
            _scooterList = new List<Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if(_scooterList.Where(x=>x.Id==id).Count() == 0)
            {
                _scooterList.Add(new Scooter(id, pricePerMinute));
            }
            else
            {
                throw new AddScooterMethodException(id);
            }
        }

        public Scooter GetScooterById(string scooterId)
        {
            if (_scooterList.Where(x => x.Id==scooterId).Count() > 0)
            {
                return _scooterList.Where(x => x.Id==scooterId).First();
            }

            throw new ScooterNotFoundException(scooterId);
        }

        public IList<Scooter> GetScooters()
        {
            return _scooterList;
        }

        public void RemoveScooter(string id)
        {
            if (_scooterList.Where(x => x.Id==id).Count() > 0)
            {
                _scooterList.Remove(_scooterList.Where(x => x.Id==id).First());
                return;
            }

            throw new ScooterNotFoundException(id);
        }
    }
}
