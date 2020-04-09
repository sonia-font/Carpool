using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class RideRequest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public RequestStatus Status { get; set; }
        public RideOffer RideOffer { get; set; }
        public User User { get; set; }
    }
}
