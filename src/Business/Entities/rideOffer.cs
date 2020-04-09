using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class RideOffer
    {        
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime RideDate { get; set; }
        public string Destiny { get; set; }
        public string Origin { get; set; }
        public string Comment { get; set; }
        public int Seats { get; set; }
        public int AvailableSeats { get; set; }
        public OfferStatus Status { get; set; }
    }
}
