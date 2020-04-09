using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class OfferData
    {
        public int Id { get; set; }
        public UserData UserData { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime RideDate { get; set; }
        public string Destiny { get; set; }
        public string Origin { get; set; }
        public string Comment { get; set; }
        public int Seats { get; set; }
        public int AvailableSeats { get; set; }
        public string Status { get; set; }
    }
}
