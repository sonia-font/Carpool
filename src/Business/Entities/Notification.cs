using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Notification
    {
        public DateTime Date { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string Message { get; set; }
        public NotificationStatus Status { get; set; }
    }
}
