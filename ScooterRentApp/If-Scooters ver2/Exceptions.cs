using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace If_Scooters_ver2
{
    [Serializable]
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException() { }

        public InvalidPriceException(decimal price) : base (String.Format("Invalid Price : {0}", price))
        {

        }
    }

    [Serializable]
    public class InvokeStartRentTimeException : Exception
    {
        public InvokeStartRentTimeException() { }

        public InvokeStartRentTimeException(DateTime InvokeStartRentTime) : base(String.Format("InvokeStartRent Not Found"))
        {

        }
    }

    [Serializable]
    public class AddScooterMethodException : Exception
    {
        public AddScooterMethodException() { }

        public AddScooterMethodException(string id) : base(String.Format("Scooter with id : {0} already exists", id))
        {

        }
    }

    [Serializable]
    public class ScooterNotFoundException : Exception
    {
        public ScooterNotFoundException() { }

        public ScooterNotFoundException(string id) : base(String.Format("Scooter with id {0} not found", id))
        {

        }
    }
}
