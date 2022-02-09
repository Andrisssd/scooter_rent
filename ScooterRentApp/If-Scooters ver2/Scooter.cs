using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace If_Scooters_ver2
{
    public class Scooter
    {
        private DateTime _invokeStartRentTime;
        /// <summary>
        /// Create new instance of the scooter.
        /// </summary>
        /// <param name="id">ID of the scooter.</param>
        /// <param name="pricePerMinute">Rental price of the scooter per one minute.</param>
        public Scooter(string id, decimal pricePerMinute)
        {
            Id = id;
            _invokeStartRentTime = default;
            if (pricePerMinute>0) 
            {
                PricePerMinute = pricePerMinute;
            }
            else
            {
                throw new InvalidPriceException(pricePerMinute);
            }
        }
        /// <summary>
        /// Unique ID of the scooter.
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Rental price of the scooter per one
        /// minute.
        /// </summary>
        public decimal PricePerMinute { get; }
        /// <summary>
        /// Identify if someone is renting this
        /// scooter.
        /// </summary>
        public bool IsRented { get; set; }

        public void SetInvokeStartRentTime(DateTime time)
        {
            _invokeStartRentTime = time;
        }

        public DateTime GetInvokeStartRentTime()
        {
            if (_invokeStartRentTime != default)
            {
                return _invokeStartRentTime;
            }
            else
            {
                throw new InvokeStartRentTimeException(_invokeStartRentTime);
            }
        }
    }
}
