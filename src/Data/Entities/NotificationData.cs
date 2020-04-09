using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class NotificationData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public UserData Sender { get; set; }
        public UserData Receiver { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
